using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
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
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace teamcare.web.app.Controllers
{
	[AuthorizeEnum(UserRoles.GlobalAdmin, UserRoles.Admin, UserRoles.StaffMember, UserRoles.ServiceUser)]
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
		private readonly IAssessmentTypeService _assessmentTypeService;
		private readonly IServiceUserDocumentService _serviceUserDocumentService;
		private readonly IHealthMedicationService _healthmedicationService;
		private IHostingEnvironment _hostingEnv;
		public Guid userName;
		private readonly IConverter _converter;

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
									  IAssessmentSkillService assessmentSkillService,
									  IAssessmentTypeService assessmentTypeService,
									  IServiceUserDocumentService serviceUserDocumentService,
									  IHostingEnvironment hostingEnv,
									  IHealthMedicationService healthMedicationService,
									  IConverter converter
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
			_assessmentTypeService = assessmentTypeService;
			_serviceUserDocumentService = serviceUserDocumentService;
			_hostingEnv = hostingEnv;
			_healthmedicationService = healthMedicationService;
			_converter = converter;
		}

		public async Task<IActionResult> Index()
		{
			SetPageMetadata(PageTitles.ServiceUsers, SiteSection.ServiceUsers, new List<BreadcrumbItem>() {
				new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
				new BreadcrumbItem(PageTitles.ServiceUsers, null),
			});

			var model = new ServiceUsersViewModel
			{
				ResidenceList = await ResidenceList()
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
				await repository.CreateAuditRecord(new Audit { Action = AuditAction.View, Details = "All ServiceUsers List.", UserReference = "", CreatedBy = base.UserId });
			});
			return View(model);
		}


		public async Task<List<SelectListItem>> ResidenceList()
		{
			var listOfResidence = await _residenceService.ListAllAsync();
			return listOfResidence.Select(x => new SelectListItem
			{
				Value = x.Id.ToString(),
				Text = x.Name
			}).OrderBy(y => y.Text).ToList();
		}

		public async Task<IActionResult> Detail(string id)
		{

			var listOfUser = await _serviceUserService.GetByIdAsync(new Guid(id));
			if (listOfUser == null) { return View(new ServiceUsersViewModel()); }
			listOfUser.PrePath = "/" + _azureStorageOptions.Container;
			listOfUser.ServiceUserLog = listOfUser.ServiceUserLog.ToList().OrderByDescending(y => y.CreatedOn).ToList();
			foreach (var item in listOfUser.Contacts)
			{
				item.PrePath = "/" + _azureStorageOptions.Container;
				item.Sequence = item.IsNextOfKin ? 1 : item.IsEmergencyContact ? 2 : 3;
			}

			foreach (var item in listOfUser.ServiceUserLog)
			{
				item.PrePath = "/" + _azureStorageOptions.Container;
			}

			SetPageMetadata(PageTitles.ServiceUsers, SiteSection.ServiceUsers, new List<BreadcrumbItem>() {
				new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
				new BreadcrumbItem(PageTitles.ServiceUsers, Url.Action("Index", "ServiceUsers")),
				new BreadcrumbItem(listOfUser.Title+" "+ listOfUser.FirstName+" "+listOfUser.LastName, null) //TODO: Replace with correct service user name
			});

			var listOfFavourite = await _favouriteServiceUserService.ListAllAsync();
			var valueOfFavourite = listOfFavourite.Where(x => x.ServiceUserId == new Guid(id) && x.UserId == (Guid)base.UserId).FirstOrDefault();
			listOfUser.Favourite = valueOfFavourite == null ? false : true;

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
					Relationship = EnumExtensions.GetEnumListItems<Relationship>(),
					ArchiveReason = EnumExtensions.GetEnumListItems<ArchiveReason>()
				},
				ContactList = listOfUser.Contacts.OrderBy(r => r.Sequence),
				IsNextOfKin = IsNextOfKin,
				IsNoEmergency = IsNoEmrgency,
				LogCategory = EnumExtensions.GetEnumListItems<ServiceUserLogCategory>()
			};
			return View(model);
		}


		[HttpGet]
		public async Task<IActionResult> AssessmentTabBind()
		{
			var assettype = await _assessmentTypeService.ListAllAsync();

			var model = new ServiceUsersViewModel
			{
				AssessmentType = assettype,
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
				var assesmentlist = await _assessmentService.ListAllAsync();
				foreach (var item in listOfUser)
				{
					item.PrePath = "/" + _azureStorageOptions.Container;
					var valueOfFavourite = listOfFavourite.Where(x => x.ServiceUserId == item.Id && x.UserId == (Guid)base.UserId).FirstOrDefault();
					item.Favourite = valueOfFavourite == null ? false : true;

					var totaldue = assesmentlist.Where(z => z.ServiceUserId == item.Id)
						.GroupBy(x => new { x.AssessmentTypeId })
						.Select(y => new
						{
							AssessmentType = y.Key.AssessmentTypeId,
							CreatedOn = y.Max(r => r.CreatedOn)
						});

					var filteres = totaldue.Where(x => x.CreatedOn.AddMonths(11) <= DateTimeOffset.UtcNow.DateTime).ToList();
					item.AssessmentDue = filteres != null ? filteres.Count : 0;
				}
			}

			var model = new ServiceUsersViewModel
			{
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
							await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = serviceUserCreateViewModel.ServiceUser.FirstName + " " + serviceUserCreateViewModel.ServiceUser.LastName + " has been created.", UserReference = "", CreatedBy = base.UserId });

						});
					}
					else
					{
						createdServiceUser = await _serviceUserService.UpdateAsync(serviceUserCreateViewModel.ServiceUser);

						_auditService.Execute(async repository =>
						{
							await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = serviceUserCreateViewModel.ServiceUser.FirstName + " " + serviceUserCreateViewModel.ServiceUser.LastName + " has been updated.", UserReference = "", CreatedBy = base.UserId });

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
							await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = "service call for add favourite serviceusers.", UserReference = "", CreatedBy = base.UserId });

						});
						return Json(new { statuscode = 1 });
					}
					else
					{
						await _favouriteServiceUserService.DeleteAsync(valueOfFavourite);
						_auditService.Execute(async repository =>
						{
							await repository.CreateAuditRecord(new Audit { Action = AuditAction.Delete, Details = "service call for remove favourite serviceusers.", UserReference = "", CreatedBy = base.UserId });

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



		public async Task<IActionResult> saveLog(string logId, string dbType, string serviceUserId, string logMessage, ServiceUserLogCategory logCategory)
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
					serviceUserLog.LogCategory = logCategory;
					serviceUserLog = await _serviceUserLogService.AddAsync(serviceUserLog);
				}
				else if (logId != null && dbType == "U")
				{
					serviceUserLog = await _serviceUserLogService.GetByIdAsync(new Guid(logId));
					if (serviceUserLog != null)
					{
						serviceUserLog.LogMessage = logMessage;
						serviceUserLog.LogCategory = logCategory;
						serviceUserLog = await _serviceUserLogService.UpdateAsync(serviceUserLog);
					}
				}
				else if (logId != null && dbType == "D")
				{
					serviceUserLog = await _serviceUserLogService.GetByIdAsync(new Guid(logId));
					if (serviceUserLog != null)
					{
						await _serviceUserLogService.DeleteAsync(serviceUserLog);
					}
				}
				blSuccess = true;
			}
			catch
			{
				blSuccess = false;
			}

			_auditService.Execute(async repository =>
			{
				await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = "Add new log for serviceusers.", UserReference = "", CreatedBy = base.UserId });

			});

			//Service User Detail for partial view 
			var listOfLog = await _serviceUserLogService.ListAllSortedFiltered(null, false, null,null);

			var listOfUser = await _serviceUserService.GetByIdAsync(new Guid(serviceUserId));
			listOfUser.PrePath = "/" + _azureStorageOptions.Container;
			listOfUser.ServiceUserLog = listOfLog.Where(x => x.LogCreatedFor == new Guid(serviceUserId)).OrderByDescending(y => y.CreatedOn).ToList();
			foreach (var item in listOfUser.ServiceUserLog)
			{
				item.PrePath = "/" + _azureStorageOptions.Container;
			}
			var model = new ServiceUsersViewModel
			{
				ServiceUserByID = listOfUser,
				LogCategory= EnumExtensions.GetEnumListItems<ServiceUserLogCategory>()
			};

			return PartialView("_ServiceUserLogList", model);
		}

		[HttpPost]
		public async Task<IActionResult> GetByLogId(Guid id)
		{
			var Logdata = await _serviceUserLogService.GetByIdAsync(id);

			//ServiceUserLogModel logtext = new ServiceUserLogModel
			//{
			//	LogMessage = Logdata.LogMessage,
			//	LogMessageUpdated = Logdata.LogMessageUpdated
			//};


			return Json(Logdata);
		}

		[HttpPost]
		public async Task<IActionResult> AssessmentGroupSkill(Guid Id, Guid ServiceUserId)
		{
			try
			{

				var assessmentlist = await _assessmentService.ListAllAsync();
				var serviceuserassessmentlist = assessmentlist.Where(r => r.ServiceUserId == ServiceUserId && r.AssessmentTypeId == Id).ToList();

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

				bool IsDue = false;
				int dueDays = 0;
				var getfirstassessment = serviceuserassessmentlist.OrderByDescending(r => r.CreatedOn).FirstOrDefault();
				if (getfirstassessment != null)
				{
					DateTimeOffset trialPeriodEnd = getfirstassessment.CreatedOn.AddMonths(11);
					DateTimeOffset todaydate = DateTimeOffset.UtcNow.DateTime;

					if (todaydate >= trialPeriodEnd)
					{
						IsDue = true;
						dueDays = (todaydate - trialPeriodEnd).Days;
					}
				}

				var SkillList = await _skillgroupService.ListAllAsync();
				var finalskill = SkillList.Where(r => r.AssessmentTypeId == Id).ToList();

				var LivingSkill = await _livingskillService.ListAllAsync();

				var assettype = await _assessmentTypeService.ListAllAsync();


				var model = new SkillAssessmentViewModel
				{
					SkillGroups = finalskill.OrderBy(r => r.Position),
					LivingSkills = LivingSkill,
					AssessmentTypeId = Id,
					AssessmentType = assettype,
					AssessmentSkillLevel = EnumExtensions.GetEnumListItems<AssessmentSkillLevel>(),
					ServiceUserId = ServiceUserId,
					AssessmentList = serviceuserassessmentlist.OrderByDescending(r => r.CreatedOn).ToList(),
					AssessmentSkill = asm,
					IsDueAssessment = IsDue,
					RiskAssessment = EnumExtensions.GetEnumListItems<RiskAssessmentLevel>(),
					DueDays = dueDays
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
						await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = "Add Assessment Type.", UserReference = "", CreatedBy = base.UserId });

					});

					if (am != null)
					{
						if (assessmentCreateViewModel?.AssessmentSkills != null)
						{
							await _assessmentkillService.SaveAssessmentSkillList((Guid)am.Id, assessmentCreateViewModel.AssessmentSkills);

							_auditService.Execute(async repository =>
							{
								await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = "Add Assessment Skill.", UserReference = "", CreatedBy = base.UserId });

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
		public async Task<IActionResult> AssessmentSkillDetails(Guid id, Guid Type, Guid ServiceUserId)
		{
			try
			{
				var model = new SkillAssessmentViewModel();

				var assessmentskillist = await _assessmentkillService.ListAllAsync();
				var finalresult = assessmentskillist.Where(p => p.AssessmentId == id).ToList();

				var SkillList = await _skillgroupService.ListAllAsync();
				var finalskill = SkillList.Where(r => r.AssessmentTypeId == Type).ToList();

				var assessmentlist = await _assessmentService.ListAllAsync();
				var serviceuserassessmentlist = assessmentlist.Where(r => r.ServiceUserId == ServiceUserId && r.AssessmentTypeId == Type).ToList();


				List<AssessmentSkillModel> asm = new List<AssessmentSkillModel>();
				foreach (var item in serviceuserassessmentlist)
				{
					var result = finalresult.Where(p => p.AssessmentId == item.Id).ToList();
					foreach (var items in result)
					{
						asm.Add(items);
					}
				}

				var assettype = await _assessmentTypeService.ListAllAsync();
				var LivingSkill = await _livingskillService.ListAllAsync();
				model = new SkillAssessmentViewModel
				{
					AssessmentTypeId = Type,
					SkillGroups = finalskill.OrderBy(r => r.Position),
					AssessmentSkill = asm,
					AssessmentType = assettype,
					LivingSkills = LivingSkill

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
				var serviceuser = await _serviceUserService.ArchiveUnArchiveUser(ReasonId, Userid, 1);

				_auditService.Execute(async repository =>
				{
					await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = "Add Archive Reason.", UserReference = "", CreatedBy = base.UserId });

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
				var serviceuser = await _serviceUserService.ArchiveUnArchiveUser(0, Userid, 2);

				_auditService.Execute(async repository =>
				{
					await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = "Unarchive.", UserReference = "", CreatedBy = base.UserId });

				});

				return Json(new { statuscode = 1 });

			}
			catch (Exception ex)
			{
				return Json(new { statuscode = 3, message = ex.Message });

			}
		}

		[HttpGet]
		public async Task<IActionResult> GetNotificationCount()
		{
			try
			{

				var listOfUser = await _serviceUserService.ListAllAsync();
				var assesmentlist = await _assessmentService.ListAllAsync();

				var totaldue = assesmentlist
					.GroupBy(x => new { x.ServiceUserId, x.AssessmentTypeId })
					.Select(y => new
					{
						ServiceUserId = y.Key.ServiceUserId,
						AssessmentType = y.Key.AssessmentTypeId,
						CreatedOn = y.Max(r => r.CreatedOn)
					});

				var filteres = totaldue.Where(x => x.CreatedOn.AddMonths(11) <= DateTimeOffset.UtcNow.DateTime)
					.GroupBy(y => y.ServiceUserId).OrderByDescending(x => x.Max(r => r.CreatedOn))
					.Select(z => z.FirstOrDefault()).ToList();

				var model = new ServiceUsersViewModel
				{
					ServiceUser = listOfUser
				};
				int totalPendingActions = 0;

				int duetotal = 0;
				foreach (var items in model.ServiceUser)
				{
					foreach (var inner in items.ServiceUserLog) { if (!inner.IsApproved) { totalPendingActions++; } }

					var getfirstassessment = filteres.Where(r => r.ServiceUserId == items.Id).FirstOrDefault();

					if (getfirstassessment != null)
					{
						duetotal++;
					}
				}

				return Json(new { PendingActions = totalPendingActions, DueAssessment = duetotal });

			}
			catch (Exception ex)
			{
				return Json(new { statuscode = 3, message = ex.Message });
			}
		}

		[HttpPost]
		public async Task<IActionResult> ServiceUserModalBind(string id)
		{
			try
			{

				var model = new ServiceUsersViewModel();

				if (!string.IsNullOrEmpty(id))
				{
					var listOfUser = await _serviceUserService.GetByIdAsync(new Guid(id));
					listOfUser.PrePath = "/" + _azureStorageOptions.Container;

					model = new ServiceUsersViewModel
					{

						ServiceUserByID = listOfUser,
						ResidenceList = await ResidenceList(),
						CreateViewModel = new ServiceUserCreateViewModel
						{
							Title = EnumExtensions.GetEnumListItems<NameTitle>(),
							Marital = EnumExtensions.GetEnumListItems<MaritalStatus>(),
							Religion = EnumExtensions.GetEnumListItems<Religion>(),
							Ethnicity = EnumExtensions.GetEnumListItems<Ethnicity>(),
							PrefLanguage = EnumExtensions.GetEnumListItems<Language>()

						}
					};
				}
				else
				{
					model = new ServiceUsersViewModel
					{

						ResidenceList = await ResidenceList(),
						CreateViewModel = new ServiceUserCreateViewModel
						{
							Title = EnumExtensions.GetEnumListItems<NameTitle>(),
							Marital = EnumExtensions.GetEnumListItems<MaritalStatus>(),
							Religion = EnumExtensions.GetEnumListItems<Religion>(),
							Ethnicity = EnumExtensions.GetEnumListItems<Ethnicity>(),
							PrefLanguage = EnumExtensions.GetEnumListItems<Language>()

						}
					};
				}
				return PartialView("~/Views/ServiceUsers/_ServiceUserCreate.cshtml", model);
			}
			catch (Exception ex)
			{
				return Json(new { statuscode = 3, message = ex.Message });

			}
		}

		[HttpGet]
		public async Task<IActionResult> DocumentTabBind(Guid userid)
		{
			var serviceUsersDocument = await _serviceUserDocumentService.ListAllAsync();
			var dTypes = EnumExtensions.GetEnumListItems<DocumentTypes>();
			var model = new ServiceUsersDocumentsViewModel
			{
				ServiceUsersDocument = serviceUsersDocument,
				UserId = userid,
				CreateViewModel = new ServiceUsersDocumentsCreateViewModel
				{
					DocumentCategories = dTypes.Where(x => x.Value != 1),
				}
			};

			return PartialView("_ServiceUsersDocumentsManagerDataContent", model);
		}

		[HttpGet]
		public async Task<IActionResult> DocumentCategoryBind(int catId, string serviceUserId = "")
		{
			if (serviceUserId == null) { serviceUserId = ""; }
			var serviceUsersDocument = await _serviceUserDocumentService.ListAllAsync();
			if (serviceUsersDocument != null && ("" + serviceUserId.ToString().Trim()) != "")
			{
				serviceUsersDocument = serviceUsersDocument.Where(x => x.ServiceUserId == new Guid(serviceUserId) && (int)x.DocumentCategory == catId).ToList();
			}
			int currentCounter = 0;
			foreach (var item in serviceUsersDocument)
			{
				item.PrePath = "/" + _azureStorageOptions.Container;
				if ((int)item.DocumentCategory == catId)
				{
					currentCounter += 1;
				}
			}
			var model = new ServiceUsersDocumentsViewModel
			{
				ServiceUsersDocument = serviceUsersDocument,
				CurrentTab = catId,
				CurrentCounter = currentCounter
			};

			return PartialView("_ServiceUsersDocumentsManagerShow", model);
		}

		[HttpGet]
		public async Task<IActionResult> GetByServiceUserDocId(string docId = "", string serviceUserId = "")
		{
			try
			{
				if (docId == null) { docId = ""; }
				if (serviceUserId == null) { serviceUserId = ""; }
				var serviceUsersDocumentByID = ("" + docId.Trim() == "") ? null : await _serviceUserDocumentService.GetByIdAsync(new Guid(docId));
				var dTypes = EnumExtensions.GetEnumListItems<DocumentTypes>();
				var model = new ServiceUsersDocumentsViewModel
				{
					ServiceUsersDocumentByID = serviceUsersDocumentByID,
					CreateViewModel = new ServiceUsersDocumentsCreateViewModel
					{
						PrePath = "/" + _azureStorageOptions.Container,
						ServiceUserDocument = serviceUsersDocumentByID,
						DocumentCategories = dTypes.Where(x => x.Value != 1)
					}
				};

				return PartialView("_ServiceUsersDocumentsManagerUpdate", model.CreateViewModel);
			}
			catch (Exception ex)
			{
				return Json(new { statuscode = 3, message = ex.Message });

			}
		}

		public async Task<IActionResult> saveServiceUserDocument(string docId, string dbType, string serviceUserId, string TempFileId, ServiceUsersDocumentsCreateViewModel data)
		{
			//IActionResult returnValue = null;
			bool blSuccess = false;
			string tmpDocId = "";
			try
			{
				ServiceUsersDocumentsModel serviceUserDoc = new ServiceUsersDocumentsModel();
				if (docId == null && dbType == "I")
				{
					data.ServiceUserDocument.CreatedBy = new Guid(serviceUserId);
					serviceUserDoc = await _serviceUserDocumentService.AddAsync(data.ServiceUserDocument);

				}
				else if (docId != null && dbType == "U")
				{
					data.ServiceUserDocument.Id = new Guid(docId);
					data.ServiceUserDocument.UpdatedBy = new Guid(serviceUserId);
					serviceUserDoc = await _serviceUserDocumentService.UpdateAsync(data.ServiceUserDocument);
				}
				else if (docId != null && dbType == "D")
				{
					data.ServiceUserDocument.Id = new Guid(docId);
					await _serviceUserDocumentService.DeleteAsync(data.ServiceUserDocument);
				}
				tmpDocId = serviceUserDoc.Id.ToString();

				if (serviceUserDoc != null && !string.IsNullOrWhiteSpace(TempFileId))
				{

					//	Get the temporary document
					var document =
						await _documentUploadService.GetByIdAsync(Guid.Parse(TempFileId));

					var relocateFile = await _fileUploadService.MoveBlobAsync(new FileUploadModel
					{
						BlobName = document.BlobName,
						DestinationFolder = serviceUserDoc.Id.ToString()
					});

					if (relocateFile != null)
					{
						document.DocumentType = (int)data.ServiceUserDocument.DocumentCategory;
						document.IsTemporary = false;
						document.ServiceUserDocumentId = serviceUserDoc.Id;
						document.BlobName = relocateFile.BlobName;

						await _documentUploadService.UpdateAsync(document);
					}
				}

				blSuccess = true;
			}
			catch (Exception ex)
			{
				blSuccess = false;
			}

			_auditService.Execute(async repository =>
			{
				await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = "Add new log for serviceusersdocument.", UserReference = "", CreatedBy = base.UserId });

			});

			//Service User Document for partial view
			if (docId == null) { docId = ""; }
			if (serviceUserId == null) { serviceUserId = ""; }
			var serviceUsersDocument = await _serviceUserDocumentService.ListAllAsync();
			var serviceUsersDocumentByID = ("" + docId.Trim() == "") ? null : await _serviceUserDocumentService.GetByIdAsync(new Guid(docId));
			int currentCounter = 0;
			if (serviceUsersDocument != null)
			{
				if (("" + serviceUserId.ToString().Trim()) != "")
				{
					serviceUsersDocument = serviceUsersDocument.Where(x => x.ServiceUserId == new Guid(serviceUserId)).ToList();
				}

				foreach (var item in serviceUsersDocument)
				{
					item.PrePath = "/" + _azureStorageOptions.Container;
					if ((int)item.DocumentCategory == (int)data.ServiceUserDocument.DocumentCategory)
					{
						currentCounter += 1;
					}
				}
			}

			var dTypes = EnumExtensions.GetEnumListItems<DocumentTypes>();
			var model = new ServiceUsersDocumentsViewModel
			{
				ServiceUsersDocument = serviceUsersDocument,
				ServiceUsersDocumentByID = serviceUsersDocumentByID,
				CurrentTab = (int)data.ServiceUserDocument.DocumentCategory,
				CurrentCounter = currentCounter,
				CreateViewModel = new ServiceUsersDocumentsCreateViewModel
				{
					DocumentCategories = dTypes.Where(x => x.Value != 1)
				}
			};

			return PartialView("_ServiceUsersDocumentsManagerShow", model);
		}

		public async Task<IActionResult> saveServiceUserDocumentDelete(string docId)
		{
			//IActionResult returnValue = null;
			bool blSuccess = false;
			try
			{
				ServiceUsersDocumentsModel data = new ServiceUsersDocumentsModel();
				data.Id = new Guid(docId);
				await _serviceUserDocumentService.DeleteAsync(data);
				blSuccess = true;
			}
			catch (Exception ex)
			{
				blSuccess = false;
				return Json(new { statuscode = 3, message = ex.Message });
			}

			_auditService.Execute(async repository =>
			{
				await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = "Add new log for serviceusersdocument.", UserReference = "", CreatedBy = base.UserId });

			});


			return Json(new { statuscode = 1, message = "Done" });
		}

		public async Task<IActionResult> generateMissingReport(string suId, string last_seen, string additional_details)
		{
			try
			{

				byte[] vs = new byte[0];
				var su = await _serviceUserService.GetByIdAsync(new Guid(suId));
				string html = "";
				string[] filePath = Directory.GetFiles(_hostingEnv.WebRootPath, "HtmlsForPdf/missingPresonReportNew.html");
				string pdfFile = Path.Combine(_hostingEnv.WebRootPath, "DownloadContent/missing_report.pdf");
				string logoimage = Request.Scheme + "://" + Request.Host.Value + "/media/logos/apple-touch-icon.png";
				string blankImage = Request.Scheme + "://" + Request.Host.Value + "media/media/avatars/blank.png";
				if (filePath.Length > 0)
				{
					using (StreamReader reader = new StreamReader(filePath[0]))
					{
						html = reader.ReadToEnd();
						if (html != null && html.Length > 0)
						{
							var nextOfKin = su.Contacts?.Where(x => x.IsNextOfKin).FirstOrDefault();
							var healthdata = await _healthmedicationService.ListAllAsync();

							var userhealth = healthdata?.Where(x => x.ServiceUserId == su.Id)?.FirstOrDefault();
							html = html.Replace("{logoImage}", logoimage);
							html = html.Replace("{name}", su.Title.ToString() + " " + su.FirstName + " " + su.LastName);
							html = html.Replace("{knownAs}", su.KnownAs);
							html = html.Replace("{dob}", su.DateOfBirth.ToString("dd/MM/yyyy"));
							html = html.Replace("{legalStatus}", su.LegalStatus);
							html = html.Replace("{nhsID}", su.NHSIdNumber);
							html = html.Replace("{nationalInsurance}", su.NationalInsuranceNo);
							html = html.Replace("{dateOfAdmission}", su.DateOfAdmission.ToString("dd/MM/yyyy"));
							html = html.Replace("{dateLastSeen}", last_seen);
							html = html.Replace("{suImage}", su.ProfilePhoto != null ? Request.Scheme + "://" + Request.Host.Value + "/" + _azureStorageOptions.Container + "/" + su.ProfilePhoto.BlobName + "?width=200" : blankImage);
							html = html.Replace("{houseTelNo}", su.Residence?.Home_Tel_No);
							html = html.Replace("{personalTelNo}", su.PersonalTelNo);
							html = html.Replace("{maritalStatus}", su.MaritalStatus.ToString());
							html = html.Replace("{religion}", su.Religion.ToString());
							html = html.Replace("{ethnicity}", su.Ethnicity.ToString());
							html = html.Replace("{language}", su.PreferredFirstLanguage.ToString());
							html = html.Replace("{occupation}", su.CurrentPreviousOccupation.ToString());
							html = html.Replace("{nextOfKin}", nextOfKin?.FirstName + " " + nextOfKin?.MiddleName + " " + nextOfKin?.LastName);
							html = html.Replace("{relationship}", nextOfKin?.Relationship.ToString());
							html = html.Replace("{address}", nextOfKin?.Address);
							html = html.Replace("{contactDetails}", nextOfKin?.Mobile);
							html = html.Replace("{otherSignificantInfo}", userhealth.OtherSignificantInformation);
							html = html.Replace("{diagnosis}", userhealth.Diagnosis);
							html = html.Replace("{additionalDetails}", additional_details);

							//// instantiate a html to pdf converter object
							//HtmlToPdf converter = new HtmlToPdf();

							//// set converter options
							//converter.Options.PdfPageSize = PdfPageSize.A4;
							//converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
							//// create a new pdf document converting an url
							//PdfDocument doc = converter.ConvertHtmlString(html, "");

							//// save pdf document
							//vs = doc.Save();


							//// close pdf document
							//doc.Close();


							var doc = new HtmlToPdfDocument()
							{
								GlobalSettings = {
									Orientation = Orientation.Portrait,
									PaperSize = PaperKind.A4
								},
								Objects = {
									new ObjectSettings() {
										HtmlContent = html,
										UseLocalLinks=true
									}
								}
							};

							vs = _converter.Convert(doc);
						}
					}
				}
				return Json(new { status = 1, pdfByteArray = vs, fileName = "missing_report.pdf", fileContentType = "application/pdf" });

			}
			catch (Exception ex)
			{
				return Json(new { status = 0, message = ex.Message });

			}
		}

		public ActionResult DownloadMissingReport()
		{
			string filePath = Path.Combine(_hostingEnv.WebRootPath, "DownloadContent/missing_report.pdf");
			string filename = "missing_report.pdf";
			byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

			return File(fileBytes, "application/pdf", filename);

		}

		public async Task<IActionResult> UpdateAbsentFromResidence(string serviceUserId, bool absentFromResidence)
		{
			try
			{
				ServiceUserModel serviceUser = await _serviceUserService.GetByIdAsync(new Guid(serviceUserId));
				if (serviceUser != null)
				{
					serviceUser.AbsentFromResidence = absentFromResidence;
					serviceUser = await _serviceUserService.UpdateAsync(serviceUser);
					return Json(new { statuscode = 1 });
				}

				return Json(new { statuscode = -1 });
			}
			catch
			{
				return Json(new { statuscode = -2 });
			}
		}
	}
}
