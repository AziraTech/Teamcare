using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.business.Services;
using teamcare.common.Configuration;
using teamcare.common.Enumerations;
using teamcare.common.Helpers;
using teamcare.common.ReferenceData;
using teamcare.data.Entities;
using teamcare.web.app.Helpers;
using teamcare.web.app.ViewModels;


namespace teamcare.web.app.Controllers
{
    [AuthorizeEnum(UserRoles.GlobalAdmin, UserRoles.Admin, UserRoles.StaffMember)]

    public class ResidenceController : BaseController
    {
        private readonly IResidenceService _residenceService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IDocumentUploadService _documentUploadService;
        private readonly AzureStorageSettings _azureStorageOptions;
        private readonly IAuditService _auditService;
        private readonly IServiceUserLogService _serviceUserLogService;

        public Guid userName;


        public ResidenceController(IResidenceService residenceService, IFileUploadService fileUploadService,
                                    IServiceUserLogService serviceUserLogService,
                                    IDocumentUploadService documentUploadService, IOptions<AzureStorageSettings> azureStorageOptions, 
                                    IAuditService auditService)
        {
            _residenceService = residenceService;
            _fileUploadService = fileUploadService;
            _documentUploadService = documentUploadService;
            _azureStorageOptions = azureStorageOptions.Value;
            _auditService = auditService;
            _serviceUserLogService = serviceUserLogService;
        }

        public async Task<IActionResult> Index()
        {
            SetPageMetadata(PageTitles.Residence, SiteSection.Residence, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.Residence, string.Empty),
            });

            //var listOfLog = await _serviceUserLogService.ListAllSortedFiltered(null, false, null);
            
            var listOfResidence = await _residenceService.ListAllResidenceFiltered(false);
            var model = new ResidenceListViewModel
            {
                Residences = listOfResidence, 
            };
            foreach (var item in model.Residences) { 
                item.PrePath = "/" + _azureStorageOptions.Container;
                item.ServiceUsers = item.ServiceUsers.Where(x => x.ArchivedOn == null).ToList();
            }

            _auditService.Execute(async repository =>
            {
                await repository.CreateAuditRecord(new Audit { Action = AuditAction.View, Details = "Get All Residence.", UserReference = "", CreatedBy = base.UserId });
            });
            return View(model);

        }

        public async Task<IActionResult> SortFilterResidence(bool isArchive)
        {

            //Sorting List
            var listOfResidences = await _residenceService.ListAllResidenceFiltered(isArchive);
            if (listOfResidences != null)
            {
                foreach (var item in listOfResidences)
                {
                    item.PrePath = "/" + _azureStorageOptions.Container;
                }
            }

            var model = new ResidenceListViewModel
            {
                Residences = listOfResidences
            };


            return PartialView("_ShowAllRecrods", model);
        }

        public async Task<IActionResult> Detail(Guid Id)
        {
            SetPageMetadata(PageTitles.Residence, SiteSection.Residence, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.Residence, Url.Action("Index", "Residence"))
            });
            var listOfResidence = await _residenceService.GetByIdAsync(Id);
            if (listOfResidence != null)
            {
                if (listOfResidence.ArchivedOn == null)
                {
                    listOfResidence.ServiceUsers = listOfResidence.ServiceUsers.Where(x => x.ArchivedOn == null).ToList();
                }
                if (listOfResidence != null) { listOfResidence.PrePath = "/" + _azureStorageOptions.Container; }
                var listOfLog = await _serviceUserLogService.ListAllSortedFiltered(null, false, null,null);
                var model = new ResidenceListViewModel
                {
                    Residence = listOfResidence, 
                    CreateViewModel=new ResidenceCreateViewModel
					{
                        ArchiveReason = EnumExtensions.GetEnumListItems<ArchiveReasonResidence>()
                    }
                };
                return View(model);
            }
            return null;
        }

        public async Task<IActionResult> SetCurrentTab(string tabName, string Id)
        {
            switch (tabName)
            {
                case "Overview": return await ResidenceDetail(new Guid(Id));
                case "Service_User": return await ServiceUserDetails(Id);
            }
            return null;
        }

        public async Task<IActionResult> ResidenceDetail(Guid Id)
        {
            ResidenceListViewModel ReturnResidenceModel = new ResidenceListViewModel();
            var listOfResidence = await _residenceService.GetByIdAsync(Id);            
            if (listOfResidence != null)
            {
                listOfResidence.ServiceUsers = listOfResidence.ServiceUsers.Where(x => x.ArchivedOn == null).ToList();
                listOfResidence.PrePath = "/" + _azureStorageOptions.Container;
                ReturnResidenceModel.Residence = listOfResidence;
                foreach (var item in ReturnResidenceModel.Residence.ServiceUsers) { item.PrePath = "/" + _azureStorageOptions.Container; }
            }
            return PartialView("_ResidenceUpdate", ReturnResidenceModel);
        }

        public async Task<IActionResult> ServiceUserDetails(string Id)
        {
            ResidenceListViewModel ReturnResidenceModel = new ResidenceListViewModel();
            var listOfResidence = await _residenceService.GetByIdAsync(new Guid(Id));            
            if (listOfResidence != null)
            {
                listOfResidence.ServiceUsers = listOfResidence.ServiceUsers.Where(x => x.ArchivedOn == null).ToList();
                listOfResidence.PrePath = "/" + _azureStorageOptions.Container;
                ReturnResidenceModel.Residence = listOfResidence;
                foreach (var item in ReturnResidenceModel.Residence.ServiceUsers) { item.PrePath = "/" + _azureStorageOptions.Container; }
            }
            return PartialView("_ResidenceServiceUserCard", ReturnResidenceModel);
        }
                
        [HttpPost]
        public async Task<IActionResult> Save(ResidenceCreateViewModel residenceCreateViewModel)
        {
            try
            {
                if (residenceCreateViewModel?.Residence != null)
                {
                    var createdResidence = await
                            ((residenceCreateViewModel.Residence.Id.ToString().Length > 5) 
                            ? _residenceService.UpdateAsync(residenceCreateViewModel.Residence)
                            : _residenceService.AddAsync(residenceCreateViewModel.Residence));
                    
                    if (createdResidence != null && !string.IsNullOrWhiteSpace(residenceCreateViewModel.TempFileId))
                    {
                        //	Get the temporary document
                        var document =
                            await _documentUploadService.GetByIdAsync(Guid.Parse(residenceCreateViewModel.TempFileId));

                        var relocateFile = await _fileUploadService.MoveBlobAsync(new FileUploadModel
                        {
                            BlobName = document.BlobName,
                            DestinationFolder = createdResidence.Id.ToString()
                        });

                        if (relocateFile != null)
                        {
                            document.DocumentType = (int)DocumentTypes.ProfilePhoto;
                            document.IsTemporary = false;
                            document.ResidenceId = createdResidence.Id;
                            document.BlobName = relocateFile.BlobName;

                            await _documentUploadService.UpdateAsync(document);
                        }

                        var returnDoc = await _residenceService.GetByIdAsync(createdResidence.Id.Value);
                    }

                    if (residenceCreateViewModel.Residence.Id.ToString().Length < 5)
                    {
                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = createdResidence.Name + " has been created.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }
                    else
                    {
                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = createdResidence.Name + " has been updated.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }
                  
                }
                if (residenceCreateViewModel.Residence.Id.ToString().Length > 5)
                {
                    return await ResidenceDetail((Guid)residenceCreateViewModel.Residence.Id);
                }

                var listOfLog = await _serviceUserLogService.ListAllSortedFiltered(null, false, null,null);
                var listOfResidence = await _residenceService.ListAllAsync();                
                var model = new ResidenceListViewModel
                {
                    Residences = listOfResidence,
                    //totalPendingActions = listOfLog.ToList().Count(x => x.IsApproved == false)
                };
                foreach (var item in model.Residences) { item.PrePath = "/" + _azureStorageOptions.Container; }                
                return PartialView("_ShowAllRecrods", model);
            } catch (Exception ex) { return Json(new { statuscode = 3, message = ex.Message }); }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                Guid id = new Guid(Id);
             
                var dum = await _documentUploadService.GetByResidenceIdAsync(id);
                if (dum != null) { await _documentUploadService.DeleteAsync(dum); }

                var residencedata = await _residenceService.GetByIdAsync(id);
                if(residencedata != null)
                {
                    await _residenceService.DeleteAsync(residencedata);

                    _auditService.Execute(async repository =>
                    {
                        await repository.CreateAuditRecord(new Audit { Action = AuditAction.Delete, Details = residencedata.Name + " has been deleted.", UserReference = "", CreatedBy = base.UserId });
                    });
                }
              

                return Json(new { success = true, statuscode = 1, message = "Removed Successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, statuscode = 2, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ArchiveResidence(int ReasonId, Guid Userid)
        {
            try
            {
                var residence = await _residenceService.ArchiveUnArchiveResidence(ReasonId, Userid, 1);
                if(residence!=null && residence.ServiceUsers !=null && residence.ServiceUsers.Where(x => x.ArchivedOn == null).Any())
				{
                    return Json(new { statuscode = 4, message = "Please! Archive Service User first to archive this residence" });
				}
                _auditService.Execute(async repository =>
                {
                    await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = "Add Archive Reason for Residence.", UserReference = "", CreatedBy = base.UserId });

                });

                return Json(new { statuscode = 1 });

            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });

            }
        }

        [HttpPost]
        public async Task<IActionResult> UnArchiveResidence(Guid Userid)
        {
            try
            {
                var serviceuser = await _residenceService.ArchiveUnArchiveResidence(0, Userid, 2);

                _auditService.Execute(async repository =>
                {
                    await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = "Unarchive residence.", UserReference = "", CreatedBy = base.UserId });

                });

                return Json(new { statuscode = 1 });

            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });

            }
        }

    }
}
