using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.business.Services;
using teamcare.common.Enumerations;
using teamcare.common.Helpers;
using teamcare.common.ReferenceData;
using teamcare.web.app.Helpers;
using teamcare.web.app.ViewModels;

namespace teamcare.web.app.Controllers
{
    [AuthorizeEnum(UserRoles.GlobalAdmin, UserRoles.Admin)]
    public class ServiceUserLogController : BaseController
    {
        private readonly IServiceUserService _serviceUserService;
        private readonly IUserService _userService;
        private readonly IServiceUserLogService _serviceUserLogService;

        public Guid userID;
        public UserModel userData;

        public ServiceUserLogModel sulm = new ServiceUserLogModel();
        public ServiceUserModel sum = new ServiceUserModel();

        public ServiceUserLogController(IServiceUserService serviceUserService,
                                    IUserService userService,
                                    IServiceUserLogService serviceUserLogService)
        {
            _serviceUserService = serviceUserService;
            _userService = userService;
            _serviceUserLogService = serviceUserLogService;

        }
        public async Task<IActionResult> Index()
        {
            SetPageMetadata(PageTitles.ServiceUserLog, SiteSection.ServiceUsers, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.ServiceUserLog, string.Empty),
            });
         
            var listOfServiceUsers = await _serviceUserService.ListAllAsync(sum);
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
                var servicelog = await _serviceUserLogService.UpdateLogByParam(1,id,status,null,(Guid)base.UserId,sulm);
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
                var servicelog = await _serviceUserLogService.UpdateLogByParam(2,id, status, null,(Guid)base.UserId, sulm);
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
                var servicelog = await _serviceUserLogService.UpdateLogByParam(3, id,false, logtext,(Guid)base.UserId, sulm);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(1);

        }

        [HttpPost]
        //
        public async Task<IActionResult> SortFilterOptionList(Guid? sortBy, bool filterBy,string daterange)
        {
           
            //Sorting List
            var listOfLog = await _serviceUserLogService.ListAllSortedFiltered(sortBy, filterBy,daterange, sulm);

            //ServiceUser List
            //var listOfServiceUsers = await _serviceUserService.ListAllAsync(sum);
            //var distinctServiceUsers = listOfServiceUsers.Select(x => new SelectListItem
            //{
            //    Value = x.Id.ToString(),
            //    Text = x.FirstName + " " + x.LastName
            //}).OrderBy(y => y.Text).ToList();

            var model = new ServiceUserLogViewModel
            {
                ServiceUserLog = listOfLog,
                //ServcieUsersList = distinctServiceUsers
            };

            return PartialView("_DataContent", model);
        }
    }
}
