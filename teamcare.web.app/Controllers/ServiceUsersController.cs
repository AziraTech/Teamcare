﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class ServiceUsersController : BaseController
    {
        private readonly IServiceUserService _serviceUserService;
        private readonly IResidenceService _residenceService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IDocumentUploadService _documentUploadService;
        private readonly IFavouriteServiceUserService _favouriteServiceUserService;
        private readonly AzureStorageSettings _azureStorageOptions;
        private readonly IServiceUserLogService _serviceUserLogService;
        private readonly IAuditService _auditService;
        private readonly ISkillGroupsService _skillgroupService;
        private readonly ILivingSkillService _livingskillService;
        private readonly IAssessmentService _assessmentService;
        private readonly IAssessmentSkillService _assessmentkillService;
        public Guid userName;

        public ServiceUsersController(IServiceUserService serviceUserService,
                                      IResidenceService residenceService,
                                      IFileUploadService fileUploadService,
                                      IDocumentUploadService documentUploadService,
                                      IFavouriteServiceUserService favouriteServiceUserService,
                                      IOptions<AzureStorageSettings> azureStorageOptions,
                                      IServiceUserLogService serviceUserLogService,
                                      IAuditService auditService,
                                      ISkillGroupsService skillgroupService,
                                      ILivingSkillService livingSkillService,
                                      IAssessmentService assessmentService,
                                      IAssessmentSkillService assessmentSkillService
                                     )
        {
            _serviceUserService = serviceUserService;
            _residenceService = residenceService;
            _fileUploadService = fileUploadService;
            _documentUploadService = documentUploadService;
            _favouriteServiceUserService = favouriteServiceUserService;
            _azureStorageOptions = azureStorageOptions.Value;
            _serviceUserLogService = serviceUserLogService;
            _auditService = auditService;
            _skillgroupService = skillgroupService;
            _livingskillService = livingSkillService;
            _assessmentService = assessmentService;
            _assessmentkillService = assessmentSkillService;

        }

        public async Task<IActionResult> Index()
        {
            SetPageMetadata(PageTitles.ServiceUsers, SiteSection.ServiceUsers, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.ServiceUsers, string.Empty),
            });

            var listOfResidence = await _residenceService.ListAllAsync();
            var distinctResidence = listOfResidence.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).OrderBy(y => y.Text).ToList();

            var model = new ServiceUsersViewModel
            {
                UserName = base.UserName,
                ResidenceList = distinctResidence,
                CreateViewModel = new ServiceUserCreateViewModel
                {
                    Title = EnumExtensions.GetEnumListItems<NameTitle>(),
                    Marital = EnumExtensions.GetEnumListItems<MaritalStatus>(),
                    Religion = EnumExtensions.GetEnumListItems<Religion>(),
                    Ethnicity = EnumExtensions.GetEnumListItems<Ethnicity>(),
                    PrefLanguage = EnumExtensions.GetEnumListItems<Language>(),
                }
            };
            if (model.ServiceUser != null)
            {
                var listOfFavourite = await _favouriteServiceUserService.ListAllAsync();
                foreach (ServiceUserModel serviceUser in model.ServiceUser)
                {
                    var valueOfFavourite = listOfFavourite.Where(x => x.ServiceUserId == serviceUser.Id && x.UserId == (Guid)base.UserId).FirstOrDefault();
                    serviceUser.Favourite = valueOfFavourite == null ? false : true;
                }
            }


            _auditService.Execute(async repository =>
            {
                await repository.CreateAuditRecord(new Audit { Action = "GetAllServiceUsers", Details = "service call for get all serviceusers.", UserReference = "", CreatedBy = base.UserId });
            });
            return View(model);
        }

        public async Task<IActionResult> Detail(string id)
        {

            var listOfUser = await _serviceUserService.GetByIdAsync(new Guid(id));
            if (listOfUser == null) { return View(new ServiceUsersViewModel()); }
            listOfUser.PrePath = "/" + _azureStorageOptions.Container;

            foreach (var item in listOfUser.Contacts)
            {
                item.PrePath = "/" + _azureStorageOptions.Container;
                item.Sequence = item.IsNextOfKin ? 1 : item.IsEmergencyContact ? 2 : 3;
            }


            SetPageMetadata(PageTitles.ServiceUsers, SiteSection.ServiceUsers, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.ServiceUsers, Url.Action("Index", "ServiceUsers")),
                new BreadcrumbItem(listOfUser.Title+" "+ listOfUser.FirstName+" "+listOfUser.LastName, null) //TODO: Replace with correct service user name
			});

            var listOfFavourite = await _favouriteServiceUserService.ListAllAsync();
            var valueOfFavourite = listOfFavourite.Where(x => x.ServiceUserId == new Guid(id)).FirstOrDefault();
            listOfUser.Favourite = valueOfFavourite == null ? false : true;
            var listOfResidence = await _residenceService.ListAllAsync();
            var distinctResidence = listOfResidence.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).OrderBy(y => y.Text).ToList();

            var model = new ServiceUsersViewModel
            {
                UserName = base.UserName,
                ServiceUserByID = listOfUser,
                ResidenceList = distinctResidence,
                CreateViewModel = new ServiceUserCreateViewModel
                {
                    Title = EnumExtensions.GetEnumListItems<NameTitle>(),
                    Marital = EnumExtensions.GetEnumListItems<MaritalStatus>(),
                    Religion = EnumExtensions.GetEnumListItems<Religion>(),
                    Ethnicity = EnumExtensions.GetEnumListItems<Ethnicity>(),
                    PrefLanguage = EnumExtensions.GetEnumListItems<Language>(),
                    Relationship = EnumExtensions.GetEnumListItems<Relationship>(),
                    ArchiveReason=EnumExtensions.GetEnumListItems<ArchiveReason>()
                },
                ContactList = listOfUser.Contacts.OrderBy(r => r.Sequence)
            };
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> AssessmentTabBind()
        {
            var model = new ServiceUsersViewModel
            {
                AssessmentType = EnumExtensions.GetEnumListItems<AssessmentType>()
            };
            return PartialView("_AssessmentDataContent", model);
        }
        public async Task<IActionResult> SortFilterOption(int sortBy, string filterBy, bool isArchive)
        {

            //Sorting List
            var listOfUser = await _serviceUserService.ListAllSortedFiltered(sortBy, filterBy, isArchive);
            if (listOfUser != null)
            {
                var listOfFavourite = await _favouriteServiceUserService.ListAllAsync();
                foreach (var item in listOfUser)
                {
                    item.PrePath = "/" + _azureStorageOptions.Container;
                    var valueOfFavourite = listOfFavourite.Where(x => x.ServiceUserId == item.Id && x.UserId == (Guid)base.UserId).FirstOrDefault();
                    item.Favourite = valueOfFavourite == null ? false : true;
                }
            }

            var model = new ServiceUsersViewModel
            {
                UserName = base.UserName,
                ServiceUser = listOfUser
            };


            return PartialView("_DataContent", model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ServiceUserCreateViewModel serviceUserCreateViewModel)
        {
            try
            {
                var createdServiceUser = new ServiceUserModel();
                if (serviceUserCreateViewModel?.ServiceUser != null)
                {                    
                    if (serviceUserCreateViewModel.ServiceUser.Id.ToString() == "")
                    {
                        createdServiceUser = await _serviceUserService.AddAsync(serviceUserCreateViewModel.ServiceUser);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = "AddServiceUsers", Details = "service call for add new serviceusers.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }
                    else
                    {
                        createdServiceUser = await _serviceUserService.UpdateAsync(serviceUserCreateViewModel.ServiceUser);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = "UpdateServiceUsers", Details = "service call for update serviceusers.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }

                    if (createdServiceUser != null && !string.IsNullOrWhiteSpace(serviceUserCreateViewModel.ServiceUser.TempFileId))
                    {

                        //	Get the temporary document
                        var document =
                            await _documentUploadService.GetByIdAsync(Guid.Parse(serviceUserCreateViewModel.ServiceUser.TempFileId));

                        var relocateFile = await _fileUploadService.MoveBlobAsync(new FileUploadModel
                        {
                            BlobName = document.BlobName,
                            DestinationFolder = createdServiceUser.Id.ToString()
                        });

                        if (relocateFile != null)
                        {
                            document.DocumentType = (int)DocumentTypes.ProfilePhoto;
                            document.IsTemporary = false;
                            document.ServiceUserId = createdServiceUser.Id;
                            document.BlobName = relocateFile.BlobName;

                            await _documentUploadService.UpdateAsync(document);
                        }

                        //var returnDoc = await _serviceUserService.GetByIdAsync(createdServiceUser.Id.Value);
                    }
                }

                if (serviceUserCreateViewModel.ServiceUser.Id.ToString() != "")
                {

                    var listOfUser = await _serviceUserService.GetByIdAsync((Guid)serviceUserCreateViewModel.ServiceUser.Id);
                    if (listOfUser == null) { return View(new ServiceUsersViewModel()); }
                    listOfUser.PrePath = "/" + _azureStorageOptions.Container;

                    foreach (var item in listOfUser.Contacts)
                    {
                        item.PrePath = "/" + _azureStorageOptions.Container;
                        item.Sequence = item.IsNextOfKin ? 1 : item.IsEmergencyContact ? 2 : 3;
                    }

                    var listOfFavourite = await _favouriteServiceUserService.ListAllAsync();
                    var valueOfFavourite = listOfFavourite.Where(x => x.ServiceUserId == (Guid)serviceUserCreateViewModel.ServiceUser.Id).FirstOrDefault();
                    listOfUser.Favourite = valueOfFavourite == null ? false : true;
                    var listOfResidence = await _residenceService.ListAllAsync();
                    var distinctResidence = listOfResidence.Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name
                    }).OrderBy(y => y.Text).ToList();

                    var model = new ServiceUsersViewModel
                    {
                        UserName = base.UserName,
                        ServiceUserByID = listOfUser,
                        ResidenceList = distinctResidence,
                        CreateViewModel = new ServiceUserCreateViewModel
                        {
                            Title = EnumExtensions.GetEnumListItems<NameTitle>(),
                            Marital = EnumExtensions.GetEnumListItems<MaritalStatus>(),
                            Religion = EnumExtensions.GetEnumListItems<Religion>(),
                            Ethnicity = EnumExtensions.GetEnumListItems<Ethnicity>(),
                            PrefLanguage = EnumExtensions.GetEnumListItems<Language>(),
                            Relationship = EnumExtensions.GetEnumListItems<Relationship>(),
                            ArchiveReason = EnumExtensions.GetEnumListItems<ArchiveReason>()
                        },
                        ContactList = listOfUser.Contacts.OrderBy(r => r.Sequence)
                    };
                    return PartialView("_SeviceUserProfileDetails", model);
                }
                return Json(new { statuscode = 1, message = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetAsFavouriteUser(string FavauriteUser)
        {
            try
            {
                userName = new Guid(User.GetClaimValue(common.ReferenceData.ClaimTypes.UserId));
                if (userName != null && FavauriteUser.Trim() != "")
                {
                    var listOfFavourite = await _favouriteServiceUserService.ListAllAsync();
                    var valueOfFavourite = listOfFavourite.Where(x => x.ServiceUserId == new Guid(FavauriteUser) && x.UserId == userName).FirstOrDefault();
                    if (valueOfFavourite == null)
                    {
                        var createdFavouriteServiceUser = new FavouriteServiceUserModel();
                        createdFavouriteServiceUser.UserId = userName;
                        createdFavouriteServiceUser.ServiceUserId = new Guid(FavauriteUser);
                        createdFavouriteServiceUser = await _favouriteServiceUserService.AddAsync(createdFavouriteServiceUser);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = "AddFavouriteServiceUsers", Details = "service call for add favourite serviceusers.", UserReference = "", CreatedBy = base.UserId });
                        });
                        return Json(new { statuscode = 1 });
                    }
                    else
                    {
                        await _favouriteServiceUserService.DeleteAsync(valueOfFavourite);
                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = "DeleteFavouriteServiceUsers", Details = "service call for remove favourite serviceusers.", UserReference = "", CreatedBy = base.UserId });
                        });

                    }
                }
                return Json(new { statuscode = 2 });
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }

        public async Task<IActionResult> saveLog(string logId, string dbType, string serviceUserId, string logMessage)
        {
            //IActionResult returnValue = null;
            bool blSuccess = false;
            try
            {

                ServiceUserLogModel serviceUserLog = new ServiceUserLogModel();
                if (logId == null && dbType == "I")
                {
                    //serviceUserLog.ActionByAdminId = (Guid)base.UserId;
                    serviceUserLog.LogCreatedFor = new Guid(serviceUserId);
                    serviceUserLog.LogMessage = logMessage;
                    serviceUserLog = await _serviceUserLogService.AddAsync(serviceUserLog);
                }
                else if (logId != null && dbType == "U")
                {
                    serviceUserLog.Id = new Guid(logId);
                    //serviceUserLog.ActionByAdminId = (Guid)base.UserId;
                    serviceUserLog.LogCreatedFor = new Guid(serviceUserId);
                    serviceUserLog.LogMessage = logMessage;
                    serviceUserLog = await _serviceUserLogService.UpdateAsync(serviceUserLog);
                }
                else if (logId != null && dbType == "D")
                {
                    serviceUserLog.Id = new Guid(logId);
                    //serviceUserLog.ActionByAdminId = (Guid)base.UserId;
                    serviceUserLog.LogCreatedFor = new Guid(serviceUserId);
                    serviceUserLog.LogMessage = logMessage;
                    await _serviceUserLogService.DeleteAsync(serviceUserLog);
                }
                blSuccess = true;
            }
            catch
            {
                blSuccess = false;
            }

            //Service User Detail for partial view 
            var listOfLog = await _serviceUserLogService.ListAllSortedFiltered(null, false, null);

            var listOfUser = await _serviceUserService.GetByIdAsync(new Guid(serviceUserId));
            listOfUser.PrePath = "/" + _azureStorageOptions.Container;
            listOfUser.ServiceUserLog = listOfUser.ServiceUserLog.ToList().OrderByDescending(y => y.CreatedOn).ToList();
            var model = new ServiceUsersViewModel
            {
                UserName = base.UserName,
                ServiceUserByID = listOfUser,
            };

            _auditService.Execute(async repository =>
            {
                await repository.CreateAuditRecord(new Audit { Action = "AddLogForServiceUsers", Details = "service call for add new log for serviceusers.", UserReference = "", CreatedBy = base.UserId });
            });

            return PartialView("_ServiceUserLogList", model);
        }

        [HttpPost]
        public async Task<IActionResult> GetByLogId(Guid id)
        {
            var Logdata = await _serviceUserLogService.GetByIdAsync(id);

            ServiceUserLogModel logtext = new ServiceUserLogModel
            {
                LogMessage = Logdata.LogMessage,
                LogMessageUpdated = Logdata.LogMessageUpdated
            };


            return Json(logtext);
        }

        [HttpPost]
        public async Task<IActionResult> AssessmentGroupSkill(int Id, Guid ServiceUserId)
        {
            try
            {
                var SkillList = await _skillgroupService.ListAllAsync();
                var finalskill = SkillList.Where(r => (int)r.AssessmentType == Id).ToList();

                var LivingSkill = await _livingskillService.ListAllAsync();

                var assessmentlist = await _assessmentService.ListAllAsync();
                var serviceuserassessmentlist = assessmentlist.Where(r => r.ServiceUserId == ServiceUserId && (int)r.AssessmentType == Id).ToList();

                var assessmentskillist = await _assessmentkillService.ListAllAsync();

                List<AssessmentSkillModel> asm = new List<AssessmentSkillModel>();
                foreach (var item in serviceuserassessmentlist)
                {
                    var result = assessmentskillist.Where(p => p.AssessmentId == item.Id).ToList();
                    foreach (var items in result)
                    {
                        asm.Add(items);
                    }
                }

                var model = new SkillAssessmentViewModel
                {
                    SkillGroups = finalskill.OrderBy(r=>r.Position),
                    LivingSkills = LivingSkill,
                    AssessmentTypeId = Id,
                    Assessment = EnumExtensions.GetEnumListItems<AssessmentSkillLevel>(),
                    ServiceUserId = ServiceUserId,
                    AssessmentList=serviceuserassessmentlist.OrderByDescending(r=>r.CreatedOn).ToList(),
                    AssessmentSkill = asm
                };

                return PartialView("_AssessmentCreate", model);
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });

            }
        }

        [HttpPost]
        public async Task<IActionResult> AssessmentSave(AssessmentViewModel assessmentCreateViewModel)
        {
            try
            {
                if (assessmentCreateViewModel?.Assessment != null)
                {
                    var am = await _assessmentService.AddAsync(assessmentCreateViewModel.Assessment);

                    _auditService.Execute(async repository =>
                    {
                        await repository.CreateAuditRecord(new Audit { Action = "AddAssessmentType", Details = "service call for add serviceuser assessment type.", UserReference = "", CreatedBy = base.UserId });
                    });

                    if (am != null)
                    {
                        if (assessmentCreateViewModel?.AssessmentSkills != null)
                        {
                            await _assessmentkillService.SaveAssessmentSkillList((Guid)am.Id, assessmentCreateViewModel.AssessmentSkills);

                            _auditService.Execute(async repository =>
                            {
                                await repository.CreateAuditRecord(new Audit { Action = "AddAssessmentSkill", Details = "service call for add serviceuser assessment skill.", UserReference = "", CreatedBy = base.UserId });
                            });
                        }
                    }
                }
                return Json(new { statuscode = 1 });

            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AssessmentSkillDetails(Guid id)
        {
            try
            {
              
                var assessmentskillist = await _assessmentkillService.ListAllAsync();
                var finalresult = assessmentskillist.Where(p => p.AssessmentId == id).ToList();
             
                var model = new SkillAssessmentViewModel
                {                  
                    AssessmentSkill = finalresult
                };

                return PartialView("_AssessmentSkillDetails", model);
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });

            }
        }

        [HttpPost]
        public async Task<IActionResult> ArchiveUserReason(int ReasonId, Guid Userid)
        {
            try
            {
                var serviceuser =  await _serviceUserService.ArchiveUnArchiveUser(ReasonId,Userid,1);

                _auditService.Execute(async repository =>
                {
                    await repository.CreateAuditRecord(new Audit { Action = "Add Archive ServiceUsers", Details = "service call for add archive serviceusers.", UserReference = "", CreatedBy = base.UserId });
                });

                return Json(new { statuscode = 1 });

            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });

            }
        }
        
        [HttpPost]
        public async Task<IActionResult> UnArchiveUser(Guid Userid)
        {
            try
            {
               var serviceuser = await _serviceUserService.ArchiveUnArchiveUser(0,Userid,2);

                _auditService.Execute(async repository =>
                {
                    await repository.CreateAuditRecord(new Audit { Action = "Un-Archive ServiceUsers", Details = "service call for un-archive serviceusers.", UserReference = "", CreatedBy = base.UserId });
                });

                return Json(new { statuscode = 1 });

            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });

            }
        }


        public async Task<int> GetPendingActions()
        {
            var listOfUser = await _serviceUserService.ListAllAsync();
            
            var model = new ServiceUsersViewModel
            {
                UserName = base.UserName,
                ServiceUser = listOfUser
            };            
            int totalPendingActions = 0;            
            foreach (var items in model.ServiceUser) { foreach (var inner in items.ServiceUserLog) { if (!inner.IsApproved) { totalPendingActions++; } } }
            return totalPendingActions;
        }
    }
}
