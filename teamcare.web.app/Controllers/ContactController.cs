using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.business.Services;
using teamcare.common.Configuration;
using teamcare.common.Enumerations;
using teamcare.common.Helpers;
using teamcare.data.Entities;
using teamcare.web.app.ViewModels;

namespace teamcare.web.app.Controllers
{
    [Authorize]
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IDocumentUploadService _documentUploadService;
        private readonly AzureStorageSettings _azureStorageOptions;
        private readonly IAuditService _auditService;
        private readonly IServiceUserService _serviceUserService;

        public Guid userName;

        public ContactController(IContactService contactService,
                                 IFileUploadService fileUploadService,
                                 IDocumentUploadService documentUploadService,
                                 IOptions<AzureStorageSettings> azureStorageOptions,
                                 IAuditService auditService,
                                 IServiceUserService serviceUserService)
        {
            _contactService = contactService;
            _fileUploadService = fileUploadService;
            _documentUploadService = documentUploadService;
            _azureStorageOptions = azureStorageOptions.Value;
            _auditService = auditService;
            _serviceUserService = serviceUserService;

        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            try
            {
                var contactData = await _contactService.GetByIdAsync(new Guid(id));
                contactData.PrePath = "/" + _azureStorageOptions.Container;

                return Json(contactData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public async Task<IActionResult> Save(ContactCreateViewModel contactCreateViewModel)
        {
            string id = "";
            try
            {
                if (contactCreateViewModel?.Contact != null)
                {

                    var createdContact = new ContactModel();

                    if (contactCreateViewModel.Contact.Id.ToString() == "")
                    {
                        var listOfContact = await _contactService.ListAllAsync();
                       
                        createdContact = await _contactService.AddAsync(contactCreateViewModel.Contact);
                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = contactCreateViewModel.Contact.FirstName + " " + contactCreateViewModel.Contact.LastName + " has been created.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }
                    else
                    {
                        createdContact = await _contactService.UpdateAsync(contactCreateViewModel.Contact);
                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = contactCreateViewModel.Contact.FirstName + " " + contactCreateViewModel.Contact.LastName + " has been updated.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }

                    if (createdContact != null && !string.IsNullOrWhiteSpace(contactCreateViewModel.TempFileId))
                    {
                        //	Get the temporary document
                        var document =
                            await _documentUploadService.GetByIdAsync(Guid.Parse(contactCreateViewModel.TempFileId));

                        var relocateFile = await _fileUploadService.MoveBlobAsync(new FileUploadModel
                        {
                            BlobName = document.BlobName,
                            DestinationFolder = createdContact.Id.ToString()
                        });

                        if (relocateFile != null)
                        {
                            document.DocumentType = (int)DocumentTypes.ProfilePhoto;
                            document.IsTemporary = false;
                            document.ContactId = createdContact.Id;
                            document.BlobName = relocateFile.BlobName;

                            await _documentUploadService.UpdateAsync(document);
                        }

                        var returnDoc = await _contactService.GetByIdAsync(createdContact.Id.Value);
                    }
                     id = createdContact.ServiceUserId.ToString();
                }
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }

            //contactCreateViewModel.Contact.ServiceUserId.ToString()
            return Json(new { statuscode = 1, message = "Sumitted Successfully...", id = id });
        }

        [HttpPost]
        public async Task<IActionResult> GetContactData(string id)
        {
            try
            {
                //Service User Detail for partial view 
                var listOfUser = await _serviceUserService.GetByIdAsync(new Guid(id));
                listOfUser.PrePath = "/" + _azureStorageOptions.Container;
                foreach (var item in listOfUser.Contacts)
                {
                    item.PrePath = "/" + _azureStorageOptions.Container;
                    item.Sequence = item.IsNextOfKin ? 1 : item.IsEmergencyContact ? 2 : 3;
                }

                bool IsNextOfKin = false;
                bool IsNoEmrgency = false;

                if (listOfUser.Contacts.Where(x => x.IsNextOfKin == true).Count() == 0)
                {
                    IsNextOfKin = true;
                }
                else if (listOfUser.Contacts.Where(x => x.IsEmergencyContact == true).Count() == 0)
                {
                    IsNoEmrgency = true;
                }

                var model = new ServiceUsersViewModel
                {
                    CreateViewModel = new ServiceUserCreateViewModel
                    {
                        Title = EnumExtensions.GetEnumListItems<NameTitle>(),
                        Relationship = EnumExtensions.GetEnumListItems<Relationship>()
                    },
                    ServiceUserByID = listOfUser,
                    ContactList = listOfUser.Contacts.OrderBy(r => r.Sequence),
                    IsNextOfKin = IsNextOfKin,
                    IsNoEmergency = IsNoEmrgency
                };
                return PartialView("~/Views/Contact/Index.cshtml", model);
            }
            catch { }
            return PartialView("~/Views/Contact/Index.cshtml", null);
        }

            [HttpPost]
        public async Task<IActionResult> Delete(Guid id, Guid serviceUserId)
        {
            try
            {
            
                var dum = await _documentUploadService.GetByContactIdAsync(id);
                if (dum != null) { await _documentUploadService.DeleteAsync(dum); }
                var contactdata = await _contactService.GetByIdAsync(id);
                if(contactdata !=null)
                {
                    await _contactService.DeleteAsync(contactdata);

                    _auditService.Execute(async repository =>
                    {
                        await repository.CreateAuditRecord(new Audit { Action = AuditAction.Delete, Details = contactdata.FirstName + " " + contactdata.LastName + " has been deleted.", UserReference = "", CreatedBy = base.UserId });
                    });
                }              

                //Service User Detail for partial view 
                var listOfUser = await _serviceUserService.GetByIdAsync(serviceUserId);
                listOfUser.PrePath = "/" + _azureStorageOptions.Container;
                listOfUser.ServiceUserLog = listOfUser.ServiceUserLog.ToList().OrderByDescending(y => y.CreatedOn).ToList();
                foreach (var item in listOfUser.Contacts)
                {
                    item.PrePath = "/" + _azureStorageOptions.Container;
                    item.Sequence = item.IsNextOfKin ? 1 : item.IsEmergencyContact ? 2 : 3;
                }
                bool IsNextOfKin = false;
                bool IsNoEmrgency = false;

                if (listOfUser.Contacts.Where(x => x.IsNextOfKin == true).Count() == 0)
                {
                    IsNextOfKin = true;
                }
                else if (listOfUser.Contacts.Where(x => x.IsEmergencyContact == true).Count() == 0)
                {
                    IsNoEmrgency = true;
                }

                var model = new ServiceUsersViewModel
                {
                    ServiceUserByID = listOfUser,
                    CreateViewModel = new ServiceUserCreateViewModel
                    {
                        Title = EnumExtensions.GetEnumListItems<NameTitle>(),                     
                        Relationship = EnumExtensions.GetEnumListItems<Relationship>()
                    },
                    ContactList = listOfUser.Contacts.OrderBy(r => r.Sequence),
                    IsNextOfKin = IsNextOfKin,
                    IsNoEmergency = IsNoEmrgency
                };
                return PartialView("~/Views/Contact/Index.cshtml", model);

            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> ContactDetailBind(string id)
        {
            try
            {
                var model = new ContactCreateViewModel();

                if (!string.IsNullOrEmpty(id))
                {
                    var contactData = await _contactService.GetByIdAsync(new Guid(id));
                    contactData.PrePath = "/" + _azureStorageOptions.Container;

                    model = new ContactCreateViewModel
                    {

                        Title = EnumExtensions.GetEnumListItems<NameTitle>(),
                        Relationship = EnumExtensions.GetEnumListItems<Relationship>(),
                        Contact = contactData
                    };
                }
                else
                {
                    model = new ContactCreateViewModel
                    {

                        Title = EnumExtensions.GetEnumListItems<NameTitle>(),
                        Relationship = EnumExtensions.GetEnumListItems<Relationship>(),
                        Contact=new ContactModel()
                    };
                }
                return PartialView("~/Views/Contact/_ContactAddUpdate.cshtml", model);
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });

            }
        }
    }
}
