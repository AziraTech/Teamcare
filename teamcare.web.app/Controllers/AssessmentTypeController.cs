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
        private readonly ISkillGroupsService _skillgroupService;
        private readonly IAssessmentService _assessmentService;
        private readonly IAssessmentSkillService _assessmentSkillService;

        public AssessmentTypeController(IAssessmentTypeService assessmenttypeService,
                                            IAuditService auditService,
                                            ILivingSkillService livingSkillService,
                                            ISkillGroupsService skillGroupsService,
                                            IAssessmentService assessmentService,
                                            IAssessmentSkillService assessmentSkillService)
        {
            _assessmenttypeservice = assessmenttypeService;
            _auditService = auditService;
            _livingSkillService = livingSkillService;
            _skillgroupService = skillGroupsService;
            _assessmentService = assessmentService;
            _assessmentSkillService = assessmentSkillService;
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
                bool IsDisable = false;

                if (!string.IsNullOrEmpty(id))
                {
                    var assetData = await _assessmenttypeservice.GetByIdAsync(new Guid(id));
                    var assessment = await _assessmentService.ListAllAsync();

                    var finalassesment = assessment.Where(r => r.AssessmentTypeId == new Guid(id)).ToList();
                    if (finalassesment.Count > 0)
                    {
                        IsDisable = true;
                    }

                    model = new AssessmentTypeCreateViewModel
                    {

                        OptionsGroup = EnumExtensions.GetEnumListItems<AssessmentOptionsGroup>(),
                        AssessmentType = assetData,
                        IsOptionsGroupDisable=IsDisable
                    };
                }
                else
                {
                    model = new AssessmentTypeCreateViewModel
                    {

                        OptionsGroup = EnumExtensions.GetEnumListItems<AssessmentOptionsGroup>(),
                        AssessmentType = new AssessmentTypeModel(),
                        IsOptionsGroupDisable = false
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
                // Assessment & AssessmentSkill
                var assessment = await _assessmentService.ListAllAsync();
                var finalassest = assessment.Where(r => r.AssessmentTypeId == id).ToList();

                var assessmentskill = await _assessmentSkillService.ListAllAsync();

                foreach (var item in finalassest)
                {
                    var skill = assessmentskill.Where(x => x.AssessmentId == item.Id).ToList();

                    foreach (var sitem in skill)
                    {
                        await _assessmentSkillService.DeleteAsync(sitem);
                    }

                    await _assessmentService.DeleteAsync(item);
                }
            

                //Group
                var groupskill = await _skillgroupService.ListAllAsync();

                foreach (var gitem in groupskill)
                {
                    var finalfroup = groupskill.Where(x => x.AssessmentTypeId == gitem.Id).FirstOrDefault();
                    if (finalfroup != null)
                    {
                        // Skill
                        var listofLvingSkill = await _livingSkillService.ListByGroupId((Guid)finalfroup.Id);

                        foreach (var item in listofLvingSkill)
                        {
                            await _livingSkillService.DeleteAsync(item);
                        }
                        await _skillgroupService.DeleteAsync(finalfroup);
                    }
                }

                // Assessment Type
                var assettype = await _assessmenttypeservice.GetByIdAsync(id);
                await _assessmenttypeservice.DeleteAsync(assettype);

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
