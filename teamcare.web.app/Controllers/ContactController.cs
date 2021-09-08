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

        public async Task<IActionResult> Detail(string id)
        {
            var contactData = await _contactService.GetByIdAsync(new Guid(id));
            contactData.PrePath = "/" + _azureStorageOptions.Container;
            return Json(contactData);
        }


        [HttpPost]
        public async Task<IActionResult> Save(ContactCreateViewModel contactCreateViewModel)
        {
            try
            {
                if (contactCreateViewModel?.Contact != null)
                {

                    var createdContact = new ContactModel();

                    if (contactCreateViewModel.Contact.Id.ToString() == "")
                    {
                        var listOfContact = await _contactService.ListAllAsync();
                        // check IfEmail already exists
                        var contact = listOfContact.FirstOrDefault(u => u.Email == contactCreateViewModel.Contact.Email);
                        if (contact != null)
                        {
                            return Json(new { statuscode = 2 });
                        }
                        createdContact = await _contactService.AddAsync(contactCreateViewModel.Contact);
                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = "AddContact", Details = "service call for add new contact.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }
                    else
                    {
                        createdContact = await _contactService.UpdateAsync(contactCreateViewModel.Contact);
                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = "UpdateContact", Details = "service call for update contact.", UserReference = "", CreatedBy = base.UserId });
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
                }
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }

            //Service User Detail for partial view 
            var listOfUser = await _serviceUserService.GetByIdAsync(contactCreateViewModel.Contact.ServiceUserId);
            listOfUser.PrePath = "/" + _azureStorageOptions.Container;
            listOfUser.ServiceUserLog = listOfUser.ServiceUserLog.ToList().OrderByDescending(y => y.CreatedOn).ToList();
            foreach (var item in listOfUser.Contacts)
            {
                item.PrePath = "/" + _azureStorageOptions.Container;
                item.Sequence = item.IsNextOfKin ? 1 : item.IsEmergencyContact ? 2 : 3;
            }
            var model = new ServiceUsersViewModel
            {
                UserName = base.UserName,
                ServiceUserByID = listOfUser,
                CreateViewModel = new ServiceUserCreateViewModel
                {
                    Title = EnumExtensions.GetEnumListItems<NameTitle>(),
                    Marital = EnumExtensions.GetEnumListItems<MaritalStatus>(),
                    Religion = EnumExtensions.GetEnumListItems<Religion>(),
                    Ethnicity = EnumExtensions.GetEnumListItems<Ethnicity>(),
                    PrefLanguage = EnumExtensions.GetEnumListItems<Language>(),
                    Relationship = EnumExtensions.GetEnumListItems<Relationship>()
                },
                ContactList = listOfUser.Contacts.OrderByDescending(r => r.Sequence)
            }; 
            return PartialView("~/Views/Contact/Index.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, Guid serviceUserId)
        {
            try
            {

                ContactModel cm = new ContactModel();
                cm.Id = id;
                cm.ServiceUserId = serviceUserId;
                cm.CreatedBy = (Guid)base.UserId;                  
                var dum = await _documentUploadService.GetByContactIdAsync(id);
                if (dum != null) { await _documentUploadService.DeleteAsync(dum); }
                await _contactService.DeleteAsync(cm);

                _auditService.Execute(async repository =>
                {
                    await repository.CreateAuditRecord(new Audit { Action = "DeleteContact", Details = "service call for delete contact.", UserReference = "", CreatedBy = base.UserId });
                });

                //Service User Detail for partial view 
                var listOfUser = await _serviceUserService.GetByIdAsync(serviceUserId);
                listOfUser.PrePath = "/" + _azureStorageOptions.Container;
                listOfUser.ServiceUserLog = listOfUser.ServiceUserLog.ToList().OrderByDescending(y => y.CreatedOn).ToList();
                foreach (var item in listOfUser.Contacts)
                {
                    item.PrePath = "/" + _azureStorageOptions.Container;
                    item.Sequence = item.IsNextOfKin ? 1 : item.IsEmergencyContact ? 2 : 3;
                }
                var model = new ServiceUsersViewModel
                {
                    UserName = base.UserName,
                    ServiceUserByID = listOfUser,
                    CreateViewModel = new ServiceUserCreateViewModel
                    {
                        Title = EnumExtensions.GetEnumListItems<NameTitle>(),
                        Marital = EnumExtensions.GetEnumListItems<MaritalStatus>(),
                        Religion = EnumExtensions.GetEnumListItems<Religion>(),
                        Ethnicity = EnumExtensions.GetEnumListItems<Ethnicity>(),
                        PrefLanguage = EnumExtensions.GetEnumListItems<Language>(),
                        Relationship = EnumExtensions.GetEnumListItems<Relationship>()
                    },
                    ContactList = listOfUser.Contacts.OrderBy(r => r.Sequence)
                };
                return PartialView("~/Views/Contact/Index.cshtml", model);

            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }

        }
    }
}
