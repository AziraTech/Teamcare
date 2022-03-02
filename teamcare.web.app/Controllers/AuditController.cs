using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Services;
using teamcare.common.Enumerations;
using teamcare.common.ReferenceData;
using teamcare.web.app.Helpers;
using teamcare.web.app.ViewModels;

namespace teamcare.web.app.Controllers
{
    [AuthorizeEnum(UserRoles.GlobalAdmin, UserRoles.Admin, UserRoles.StaffMember)]
    public class AuditController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IAuditService _auditService;

        public AuditController(IUserService userService,
                                 IAuditService auditService)
        {
            _userService = userService;
            _auditService = auditService;

        }

        public async Task<IActionResult> Index(Guid id)
        {
            SetPageMetadata(PageTitles.Audit, SiteSection.ServiceUsers, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.Audit, "0"),
            });

            var listOfUsers = await _userService.ListAllAsync();
            var distinctUsers = listOfUsers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FirstName + " " + x.LastName
            }).OrderBy(y => y.Text).ToList();
           
            var model = new AuditViewModel
            {
                ServcieUsersList = distinctUsers,
                UserId=id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SortFilterOptionList(Guid? filterByserviceuser, string daterange)
        {
            try
            {

                var listOfAudit = await _auditService.ListAllSortedFiltered(filterByserviceuser, daterange);

                foreach (var item in listOfAudit)
                {
                    var user = await _userService.GetByIdAsync((Guid)item.CreatedBy);
                    if (item.CreatedBy == user.Id)
                    {
                        item.UserName = user.Title + " " + user.FirstName + " " + user.LastName;
                    }
                }

                var model = new AuditViewModel
                {
                    Audit = listOfAudit.OrderByDescending(x => x.CreatedOn).ToList(),
                    UserId= (Guid)base.UserId
                };

                return PartialView("_AuditDataContent", model);

            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }
    }
}
