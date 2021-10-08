using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.business.Services;
using teamcare.common.Enumerations;
using teamcare.common.Helpers;
using teamcare.common.ReferenceData;
using teamcare.data.Entities;
using teamcare.web.app.Helpers;
using teamcare.web.app.ViewModels;

namespace teamcare.web.app.Controllers
{
    //[AuthorizeEnum(UserRoles.GlobalAdmin, UserRoles.Admin)]

    public class AssessmentTypeController : BaseController
    {
        private readonly IAssessmentTypeService _assessmenttypeservice;
        private readonly ILivingSkillService _livingSkillService;
        private readonly IAuditService _auditService;

        public AssessmentTypeController(IAssessmentTypeService assessmenttypeService,
                                            IAuditService auditService,
                                            ILivingSkillService livingSkillService)
        {
            _assessmenttypeservice = assessmenttypeService;
            _auditService = auditService;
            _livingSkillService = livingSkillService;

        }

        public async Task<IActionResult> Index()
        {
            SetPageMetadata(PageTitles.Assessment, SiteSection.Users, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.Assessment,null),
            });
    
            var model = new AssessmentTypeCreateViewModel
            {
                OptionsGroup = EnumExtensions.GetEnumListItems<AssessmentOptionsGroup>(),

            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AssessmentTypeBind(string id)
        {
            try
            {
                var model = new AssessmentTypeCreateViewModel();

                if (!string.IsNullOrEmpty(id))
                {
                    var assetData = await _assessmenttypeservice.GetByIdAsync(new Guid(id));

                    model = new AssessmentTypeCreateViewModel
                    {

                        OptionsGroup = EnumExtensions.GetEnumListItems<AssessmentOptionsGroup>(),
                        AssessmentType = assetData
                    };
                }
                else
                {
                    model = new AssessmentTypeCreateViewModel
                    {

                       OptionsGroup= EnumExtensions.GetEnumListItems<AssessmentOptionsGroup>(),
                        AssessmentType = new AssessmentTypeModel()
                    };
                }
                return PartialView("~/Views/AssessmentType/_AssessmentTypeAddUpdate.cshtml", model);
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });

            }
        }

        [HttpPost]
        public async Task<IActionResult> BindType()
        {
            var assettype = await _assessmenttypeservice.ListAllAsync();
           
            var model = new AssessmentTypeCreateViewModel
            {
                AssessmentTypeList = assettype,
            };

            return PartialView("_AssessmentTypeList", model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(AssessmentTypeCreateViewModel assessmentTypeCreateViewModel)
        {
            try
            {
                if (assessmentTypeCreateViewModel?.AssessmentType != null)
                {
                    var created = new AssessmentTypeModel();

                    var listOfSkillGroup = await _assessmenttypeservice.ListAllAsync();

                    // check Group Name already exists
                    var groupdata = listOfSkillGroup.FirstOrDefault(u => u.Id != assessmentTypeCreateViewModel.AssessmentType.Id && u.TypeName.Trim().ToLower() == assessmentTypeCreateViewModel.AssessmentType.TypeName.Trim().ToLower());
                    if (groupdata != null)
                    {
                        return Json(new { statuscode = 2 });
                    }

                    if (assessmentTypeCreateViewModel.AssessmentType.Id.ToString() == "")
                    {

                        if (listOfSkillGroup.Count() != 0)
                        {
                            int max = listOfSkillGroup.ToList().OrderByDescending(p => p.Position).FirstOrDefault().Position;
                            assessmentTypeCreateViewModel.AssessmentType.Position = max + 1;
                        }
                        else
                        {
                            assessmentTypeCreateViewModel.AssessmentType.Position = 0;
                        }                      

                        created = await _assessmenttypeservice.AddAsync(assessmentTypeCreateViewModel.AssessmentType);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = "Add new assessment type.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }
                    else
                    {
                        created = await _assessmenttypeservice.UpdateAsync(assessmentTypeCreateViewModel.AssessmentType);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = "Update assessment type.", UserReference = "", CreatedBy = base.UserId });
                        });
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
        public async Task<IActionResult> MovePosition(AssessmentTypeCreateViewModel assessmentTypeCreateViewModel)
        {
            try
            {
                if (assessmentTypeCreateViewModel?.AssessmentTypeList != null)
                {

                     await _assessmenttypeservice.ChangePositoin(assessmentTypeCreateViewModel.AssessmentTypeList);
                  
                    _auditService.Execute(async repository =>
                    {
                        await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = "Move Assessment Type Position.", UserReference = "", CreatedBy = base.UserId });
                    });
                }

                return Json(new { statuscode = 1 });

            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAssessmentType(Guid id)
        {
            try 
            {
                var listofLvingSkill = await _livingSkillService.ListByGroupId(id);

                foreach (var item in listofLvingSkill)
                {
                    await _livingSkillService.DeleteAsync(item);
                }

                var groupskill = await _assessmenttypeservice.GetByIdAsync(id);
                await _assessmenttypeservice.DeleteAsync(groupskill);

                _auditService.Execute(async repository =>
                {
                    await repository.CreateAuditRecord(new Audit { Action = AuditAction.Delete, Details = "Delete assessment type.", UserReference = "", CreatedBy = base.UserId });
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
