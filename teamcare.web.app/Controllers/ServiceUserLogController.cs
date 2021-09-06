﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    [AuthorizeEnum(UserRoles.GlobalAdmin, UserRoles.Admin)]
    public class ServiceUserLogController : BaseController
    {
        private readonly IServiceUserService _serviceUserService;
        private readonly IServiceUserLogService _serviceUserLogService;
        private readonly IAuditService _auditService;

        public ServiceUserLogController(IServiceUserService serviceUserService,
                                    IServiceUserLogService serviceUserLogService,
                                    IAuditService auditService)
        {
            _serviceUserService = serviceUserService;
            _serviceUserLogService = serviceUserLogService;
            _auditService = auditService;

        }
        public async Task<IActionResult> Index()
        {
            SetPageMetadata(PageTitles.ServiceUserLog, SiteSection.ServiceUsers, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.ServiceUserLog, string.Empty),
            });
         
            var listOfServiceUsers = await _serviceUserService.ListAllAsync();
            var distinctServiceUsers = listOfServiceUsers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FirstName + " " + x.LastName
            }).OrderBy(y => y.Text).ToList();

            var model = new ServiceUserLogViewModel
            {
                ServcieUsersList = distinctServiceUsers
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IsApprove(Guid id,bool status)
        {
            try
            {              
                var servicelog = await _serviceUserLogService.UpdateLogByParam(1,id,status,null,(Guid)base.UserId);

                _auditService.Execute(async repository =>
                {
                    await repository.CreateAuditRecord(new Audit { Action = "SetLogStatus", Details = "service call for log approve/Unapprove.", UserReference = "" });
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(1);
        }

        [HttpPost]
        public async Task<IActionResult> IsInVisible(Guid id, bool status)
        {
            try
            {   
                var servicelog = await _serviceUserLogService.UpdateLogByParam(2,id, status, null,(Guid)base.UserId);
                _auditService.Execute(async repository =>
                {
                    await repository.CreateAuditRecord(new Audit { Action = "SetLogStatus", Details = "service call for log hide/show.", UserReference = "" });
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(1);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLog(Guid id,String logtext)
        {
            try
            {
                var servicelog = await _serviceUserLogService.UpdateLogByParam(3, id,false, logtext,(Guid)base.UserId);

                _auditService.Execute(async repository =>
                {
                    await repository.CreateAuditRecord(new Audit { Action = "UpdateLogEntry for " + id, Details = "service call for update log entry by admin.", UserReference = "" });
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(1);

        }

        [HttpPost]
        //
        public async Task<IActionResult> SortFilterOptionList(Guid? filterByserviceuser, bool IsApprove,string daterange)
        {
           
            //Sorting List
            var listOfLog = await _serviceUserLogService.ListAllSortedFiltered(filterByserviceuser, IsApprove, daterange);

            var model = new ServiceUserLogViewModel
            {
                ServiceUserLog = listOfLog,
            };

            _auditService.Execute(async repository =>
            {
                await repository.CreateAuditRecord(new Audit { Action = "GetAllLog", Details = "service call for get all log entry.", UserReference = "" });
            });
            return PartialView("_DataContent", model);
        }
    }
}
