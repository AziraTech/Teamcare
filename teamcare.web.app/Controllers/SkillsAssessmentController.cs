using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Services;
using teamcare.common.Enumerations;
using teamcare.common.ReferenceData;
using teamcare.data.Entities;
using teamcare.web.app.Helpers;
using teamcare.web.app.ViewModels;

namespace teamcare.web.app.Controllers
{
    //[AuthorizeEnum(UserRoles.GlobalAdmin, UserRoles.Admin)]

    public class SkillsAssessmentController : BaseController
    {
        private readonly ISkillGroupsService _skillGroupService;
        private readonly ILivingSkillService _livingskillService;
        private readonly IAuditService _auditService;

        public SkillsAssessmentController(ISkillGroupsService skillGroupService,
                                            ILivingSkillService livingskillService,
                                            IAuditService auditService)
        {
            _skillGroupService = skillGroupService;
            _livingskillService = livingskillService;
            _auditService = auditService;

        }

        public async Task<IActionResult> Index()
        {
            SetPageMetadata(PageTitles.SkillAssest, SiteSection.Users, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.SkillAssest, string.Empty),
            });

            var listOfSkillGroup = await _skillGroupService.ListAllAsync();

            var model = new SkillAssessmentViewModel
            {
                SkillGroups = listOfSkillGroup.ToList().OrderBy(p => p.Position),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(SkillAssessmentViewModel skillAssessmentCreateViewModel)
        {
            try
            {
                if (skillAssessmentCreateViewModel?.SkillGroup != null)
                {

                    var listOfSkillGroup = await _skillGroupService.ListAllAsync();
                    if (listOfSkillGroup.Count() != 0)
                    {
                        int max = listOfSkillGroup.ToList().OrderByDescending(p => p.Position).FirstOrDefault().Position;
                        skillAssessmentCreateViewModel.SkillGroup.Position = max + 1;
                    }
                    else
                    {
                        skillAssessmentCreateViewModel.SkillGroup.Position = 0;
                    }

                    var created = await _skillGroupService.AddAsync(skillAssessmentCreateViewModel.SkillGroup);

                    _auditService.Execute(async repository =>
                    {
                        await repository.CreateAuditRecord(new Audit { Action = "AddSkillGroup", Details = "service call for add new skill group.", UserReference = "", CreatedBy = base.UserId });
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
        public async Task<IActionResult> MovePosition(SkillAssessmentViewModel skillAssessmentCreateViewModel)
        {
            try
            {
                if (skillAssessmentCreateViewModel?.SkillGroups != null)
                {

                    foreach (var item in skillAssessmentCreateViewModel.SkillGroups)
                    {
                        var skilldata = await _skillGroupService.GetByIdAsync((Guid)item.Id);
                        if (skilldata != null)
                        {
                            skilldata.Position = item.Position;
                        }

                        var skillgroup = await _skillGroupService.UpdateAsync(skilldata);

                    }

                    _auditService.Execute(async repository =>
                    {
                        await repository.CreateAuditRecord(new Audit { Action = "Move SkillGroup Position", Details = "service call for move skill group position.", UserReference = "", CreatedBy = base.UserId });
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
        public async Task<IActionResult> SaveLivingSkill(SkillAssessmentViewModel skillAssessmentCreateViewModel)
        {
            try
            {
                if (skillAssessmentCreateViewModel?.LivingSkill != null)
                {

                    var listOfSkill = await _livingskillService.ListAllAsync();
                    if (listOfSkill.Count() != 0)
                    {
                        int max = listOfSkill.ToList().OrderByDescending(p => p.Position).FirstOrDefault().Position;
                        skillAssessmentCreateViewModel.LivingSkill.Position = max + 1;
                    }
                    else
                    {
                        skillAssessmentCreateViewModel.LivingSkill.Position = 0;
                    }

                    var created = await _livingskillService.AddAsync(skillAssessmentCreateViewModel.LivingSkill);

                    _auditService.Execute(async repository =>
                    {
                        await repository.CreateAuditRecord(new Audit { Action = "AddLivingSkill", Details = "service call for add new living skill.", UserReference = "", CreatedBy = base.UserId });
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
        public async Task<IActionResult> LivingList(Guid Id)
        {
            var listofSkill = await _livingskillService.ListAllAsync();
            var finalskilllist = listofSkill.Where(r => r.GroupId == Id).OrderBy(p => p.Position).ToList();
            var model = new SkillAssessmentViewModel
            {
                LivingSkills = finalskilllist,
            };

            return PartialView("~/Views/SkillsAssessment/_LivingSkillList.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> LivingMovePosition(SkillAssessmentViewModel skillAssessmentCreateViewModel)
        {
            try
            {
                if (skillAssessmentCreateViewModel?.LivingSkills != null)
                {

                    foreach (var item in skillAssessmentCreateViewModel.LivingSkills)
                    {
                        var skilldata = await _livingskillService.GetByIdAsync((Guid)item.Id);
                        if (skilldata != null)
                        {
                            skilldata.Position = item.Position;
                        }

                        var skillgroup = await _livingskillService.UpdateAsync(skilldata);

                    }

                    _auditService.Execute(async repository =>
                    {
                        await repository.CreateAuditRecord(new Audit { Action = "Move LivingSkill Position", Details = "service call for move living skill position.", UserReference = "", CreatedBy = base.UserId });
                    });
                }


                return Json(new { statuscode = 1 });

            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }
    }


}
