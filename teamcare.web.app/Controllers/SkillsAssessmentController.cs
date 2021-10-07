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

    public class SkillsAssessmentController : BaseController
    {
        private readonly ISkillGroupsService _skillGroupService;
        private readonly ILivingSkillService _livingskillService;
        private readonly IAuditService _auditService;
        private readonly IAssessmentSkillService _assessmentSkillService;

        public SkillsAssessmentController(ISkillGroupsService skillGroupService,
                                            ILivingSkillService livingskillService,
                                            IAuditService auditService,
                                            IAssessmentSkillService assessmentSkillService)
        {
            _skillGroupService = skillGroupService;
            _livingskillService = livingskillService;
            _auditService = auditService;
            _assessmentSkillService = assessmentSkillService;

        }

        public async Task<IActionResult> Index()
        {
            SetPageMetadata(PageTitles.SkillAssest, SiteSection.Users, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.SkillAssest, string.Empty),
            });
    
            var model = new SkillAssessmentViewModel
            {
                Assessment = EnumExtensions.GetEnumListItems<AssessmentType>(),
                AssessmentOptionsGroup = EnumExtensions.GetEnumListItems<AssessmentOptionsGroup>()

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BindGroup(int TypeId)
        {
            var listOfSkillGroup = await _skillGroupService.ListAllAsync();
            var TypeWiseGroupList = listOfSkillGroup.Where(x => (int)x.AssessmentType == TypeId).ToList();

            foreach (var item in TypeWiseGroupList)
            {
                var listOfSkill = await _livingskillService.ListByGroupId((Guid)item.Id);
                int total = listOfSkill.Count();
                item.TotalSkill = total;
            }

            var model = new SkillAssessmentViewModel
            {
                SkillGroups = TypeWiseGroupList.ToList().OrderBy(p => p.Position),
            };

            return PartialView("_SkillGroupList", model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(SkillAssessmentViewModel skillAssessmentCreateViewModel)
        {
            try
            {
                if (skillAssessmentCreateViewModel?.SkillGroup != null)
                {
                    var created = new SkillGroupsModel();

                    var listOfSkillGroup = await _skillGroupService.ListAllAsync();

                    // check Group Name already exists
                    var groupdata = listOfSkillGroup.FirstOrDefault(u => u.Id != skillAssessmentCreateViewModel.SkillGroup.Id && u.GroupName.Trim().ToLower() == skillAssessmentCreateViewModel.SkillGroup.GroupName.Trim().ToLower() && u.AssessmentType == skillAssessmentCreateViewModel.SkillGroup.AssessmentType);
                    if (groupdata != null)
                    {
                        return Json(new { statuscode = 2 });
                    }

                    if (skillAssessmentCreateViewModel.SkillGroup.Id.ToString() == "")
                    {

                        if (listOfSkillGroup.Count() != 0)
                        {
                            int max = listOfSkillGroup.ToList().OrderByDescending(p => p.Position).FirstOrDefault().Position;
                            skillAssessmentCreateViewModel.SkillGroup.Position = max + 1;
                        }
                        else
                        {
                            skillAssessmentCreateViewModel.SkillGroup.Position = 0;
                        }                      

                        created = await _skillGroupService.AddAsync(skillAssessmentCreateViewModel.SkillGroup);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = "service call for add new skill group.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }
                    else
                    {
                        created = await _skillGroupService.UpdateAsync(skillAssessmentCreateViewModel.SkillGroup);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = "service call for update skill group.", UserReference = "", CreatedBy = base.UserId });
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
        public async Task<IActionResult> MovePosition(SkillAssessmentViewModel skillAssessmentCreateViewModel)
        {
            try
            {
                if (skillAssessmentCreateViewModel?.SkillGroups != null)
                {

                     await _skillGroupService.ChangePositoin(skillAssessmentCreateViewModel.SkillGroups);
                  
                    _auditService.Execute(async repository =>
                    {
                        await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = "service call for move skill group position.", UserReference = "", CreatedBy = base.UserId });
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
        public async Task<IActionResult> DeleteSkillGroup(Guid id)
        {
            try 
            {
                var listofLvingSkill = await _livingskillService.ListByGroupId(id);

                foreach (var item in listofLvingSkill)
                {
                    await _livingskillService.DeleteAsync(item);
                }

                var groupskill = await _skillGroupService.GetByIdAsync(id);
                await _skillGroupService.DeleteAsync(groupskill);

                _auditService.Execute(async repository =>
                {
                    await repository.CreateAuditRecord(new Audit { Action = AuditAction.Delete, Details = "service call for delete skill group.", UserReference = "", CreatedBy = base.UserId });
                });

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
                    var created = new LivingSkillsModel();

                    var listOfSkill = await _livingskillService.ListAllAsync();
                    // check Skill Name already exists
                    var skilldata = listOfSkill.FirstOrDefault(u => u.Id != skillAssessmentCreateViewModel.LivingSkill.Id && u.SkillName.Trim().ToLower() == skillAssessmentCreateViewModel.LivingSkill.SkillName.Trim().ToLower() && u.GroupId == skillAssessmentCreateViewModel.LivingSkill.GroupId);
                    if (skilldata != null)
                    {
                        return Json(new { statuscode = 2 });
                    }

                    if (skillAssessmentCreateViewModel.LivingSkill.Id.ToString() == "")
                    {
                       

                        if (listOfSkill.Count() != 0)
                        {
                            int max = listOfSkill.ToList().OrderByDescending(p => p.Position).FirstOrDefault().Position;
                            skillAssessmentCreateViewModel.LivingSkill.Position = max + 1;
                        }
                        else
                        {
                            skillAssessmentCreateViewModel.LivingSkill.Position = 0;
                        }

                        created = await _livingskillService.AddAsync(skillAssessmentCreateViewModel.LivingSkill);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = "service call for add new living skill.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }
                    else
                    {
                        created = await _livingskillService.UpdateAsync(skillAssessmentCreateViewModel.LivingSkill);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = "service call for update living skill.", UserReference = "", CreatedBy = base.UserId });
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

                    await _livingskillService.ChangePositoin(skillAssessmentCreateViewModel.LivingSkills);

                    _auditService.Execute(async repository =>
                    {
                        await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = "service call for move living skill position.", UserReference = "", CreatedBy = base.UserId });
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
        public async Task<IActionResult> DeleteLivingSkill(Guid id)
        {
            try
            {
                await _livingskillService.DeleteSetNullSkillId(id);
                            
                _auditService.Execute(async repository =>
                {
                    await repository.CreateAuditRecord(new Audit { Action = AuditAction.Delete, Details = "service call for delete living skill.", UserReference = "", CreatedBy = base.UserId });
                });

                return Json(new { statuscode = 1 });

            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditSkillGroup(Guid Id)
        {
            var listofSkill = await _skillGroupService.GetByIdAsync(Id);
            var model = new SkillAssessmentViewModel
            {
                SkillGroup = listofSkill,
            };

            return PartialView("~/Views/SkillsAssessment/_SkillGroupUpdate.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditLivingSkill(Guid Id)
        {
            var listofliving = await _livingskillService.GetByIdAsync(Id);
            var skillgroup = await _skillGroupService.GetByIdAsync(listofliving.GroupId);
            listofliving.GroupName = skillgroup.GroupName;

            var model = new SkillAssessmentViewModel
            {
                LivingSkill = listofliving,
            };

            return PartialView("~/Views/SkillsAssessment/_LivingSkillUpdate.cshtml", model);
        }

    }


}
