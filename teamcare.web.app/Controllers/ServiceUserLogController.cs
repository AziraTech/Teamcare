using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teamcare.business.Services;
using teamcare.common.Configuration;
using teamcare.common.Enumerations;
using teamcare.common.ReferenceData;
using teamcare.data.Entities;
using teamcare.web.app.Helpers;
using teamcare.web.app.ViewModels;

namespace teamcare.web.app.Controllers
{
    //[AuthorizeEnum(UserRoles.GlobalAdmin, UserRoles.Admin)]
    public class ServiceUserLogController : BaseController
    {
        private readonly IServiceUserService _serviceUserService;
        private readonly IServiceUserLogService _serviceUserLogService;
        private readonly IAuditService _auditService;
        private readonly AzureStorageSettings _azureStorageOptions;

        public ServiceUserLogController(IServiceUserService serviceUserService,
                                    IServiceUserLogService serviceUserLogService,
                                    IAuditService auditService,
                                      IOptions<AzureStorageSettings> azureStorageOptions
                                    )
        {
            _serviceUserService = serviceUserService;
            _serviceUserLogService = serviceUserLogService;
            _auditService = auditService;
            _azureStorageOptions = azureStorageOptions.Value;


        }
        public async Task<IActionResult> Index()
        {
            SetPageMetadata(PageTitles.ServiceUserLog, SiteSection.ServiceUsers, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.ServiceUserLog, "0"),
            });

            var listOfServiceUsers = await _serviceUserService.ListAllAsync();
            var distinctServiceUsers = listOfServiceUsers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FirstName + " " + x.LastName
            }).OrderBy(y => y.Text).ToList();


            var listOfLog = await _serviceUserLogService.ListAllSortedFiltered(null, false, null);

            var model = new ServiceUserLogViewModel
            {
                ServcieUsersList = distinctServiceUsers, 
            };

            _auditService.Execute(async repository =>
            {
                await repository.CreateAuditRecord(new Audit { Action = AuditAction.View, Details = "service call for get all log entry.", UserReference = "", CreatedBy = base.UserId });
            });

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IsApprove(Guid id, bool status)
        {
            try
            {
                var servicelog = await _serviceUserLogService.UpdateLogByParam(1, id, status, null, (Guid)base.UserId);

                _auditService.Execute(async repository =>
                {
                    await repository.CreateAuditRecord(new Audit { Action = AuditAction.StatusChange, Details = "service call for log approve/Unapprove.", UserReference = "", CreatedBy = base.UserId });
                });

                return Json(new { statuscode = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> IsInVisible(Guid id, bool status)
        {
            try
            {
                var servicelog = await _serviceUserLogService.UpdateLogByParam(2, id, status, null, (Guid)base.UserId);
                
                _auditService.Execute(async repository =>
                {
                    await repository.CreateAuditRecord(new Audit { Action = AuditAction.StatusChange, Details = "service call for log hide/show.", UserReference = "", CreatedBy = base.UserId });
                });

                return Json(new { statuscode = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLog(Guid id, String logtext)
        {
            try
            {
                var servicelog = await _serviceUserLogService.UpdateLogByParam(3, id, false, logtext, (Guid)base.UserId);

                _auditService.Execute(async repository =>
                {
                    await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = "service call for update log entry by admin.", UserReference = "", CreatedBy = base.UserId });
                });

                return Json(new { statuscode = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }

        }

        [HttpPost]
        public async Task<IActionResult> SortFilterOptionList(Guid? filterByserviceuser, bool IsApprove, string daterange)
        {

            var listOfLog = await _serviceUserLogService.ListAllSortedFiltered(filterByserviceuser, IsApprove, daterange);

            foreach (var item in listOfLog)
            {
                item.PrePath = "/" + _azureStorageOptions.Container;
            }

            var model = new ServiceUserLogViewModel
            {
                ServiceUserLog = listOfLog, 
            };

            return PartialView("_DataContent", model);
        }

    }
}
