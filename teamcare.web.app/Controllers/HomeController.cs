using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using teamcare.common.Enumerations;
using teamcare.common.ReferenceData;
using teamcare.web.app.ViewModels;
using teamcare.business.Services;
using System;
using teamcare.common.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.Linq;
using teamcare.data.Entities;

namespace teamcare.web.app.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFavouriteServiceUserService _favouriteServiceUserService;
        private readonly IServiceUserService _serviceUserService;
        private readonly AzureStorageSettings _azureStorageOptions;
        private readonly IAuditService _auditService;
        private readonly IServiceUserLogService _serviceUserLogService;

        public HomeController(ILogger<HomeController> logger,
                              IServiceUserService serviceUserService,
                              IFavouriteServiceUserService favouriteServiceUserService,
                              IServiceUserLogService serviceUserLogService,
                              IOptions<AzureStorageSettings> azureStorageOptions, IAuditService auditService)
        {
            _logger = logger;
            _favouriteServiceUserService = favouriteServiceUserService;
            _serviceUserService = serviceUserService;
            _azureStorageOptions = azureStorageOptions.Value;
            _auditService = auditService;
            _serviceUserLogService = serviceUserLogService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            SetPageMetadata(PageTitles.Dashboard, SiteSection.Dashboard, new List<BreadcrumbItem>()
                {
                    new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home"))
                });
    
            var listOfUsers = await _serviceUserService.ListAllSortedFiltered(0, null);
            if (listOfUsers != null)
            {
                var listOfFavourite = await _favouriteServiceUserService.ListAllAsync();
                foreach (var item in listOfUsers)
                {
                    item.PrePath = "/" + _azureStorageOptions.Container;
                    var valueOfFavourite = listOfFavourite.Where(x => x.ServiceUserId == item.Id && x.UserId == (Guid)base.UserId).FirstOrDefault();
                    item.Favourite = valueOfFavourite == null ? false : true;
                }
            }

            var listOfLog = await _serviceUserLogService.ListAllSortedFiltered(null, false, null);
            
            var model = new HomeViewModel
            { 
                ServiceUser = listOfUsers,
                totalPendingActions = listOfLog.ToList().Count(x => x.IsApproved == false)
            };
            
            _auditService.Execute(async repository =>
            {
                await repository.CreateAuditRecord(new Audit { Action = "Home page", Details = "User has accessed Home page", UserReference = "", CreatedBy = base.UserId });
            });
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

