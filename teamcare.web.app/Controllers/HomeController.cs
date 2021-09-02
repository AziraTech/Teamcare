﻿using System.Collections.Generic;
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
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using teamcare.business.Models;

namespace teamcare.web.app.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IResidenceService _residenceService;
        private readonly IFavouriteServiceUserService _favouriteServiceUserService;
        private readonly IServiceUserService _serviceUserService;
        private readonly AzureStorageSettings _azureStorageOptions;
        private readonly IUserService _userService;
        public Guid userName;

        public HomeController(ILogger<HomeController> logger,
                              IUserService userService,
                              IResidenceService residenceService,
                              IServiceUserService serviceUserService,
                              IFavouriteServiceUserService favouriteServiceUserService,
                              IOptions<AzureStorageSettings> azureStorageOptions)
        {
            _logger = logger;
            _residenceService = residenceService;
            _favouriteServiceUserService = favouriteServiceUserService;
            _serviceUserService = serviceUserService;
            _azureStorageOptions = azureStorageOptions.Value;
            _userService = userService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            SetPageMetadata(PageTitles.Dashboard, SiteSection.Dashboard, new List<BreadcrumbItem>() {
                    new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home"))
                    });


            var um = new ServiceUserModel();
            var fsum = new FavouriteServiceUserModel();

            var listOfUsers = await _serviceUserService.ListAllSortedFiltered(0, null,um);
            if (listOfUsers != null)
            {
                var listOfFavourite = await _favouriteServiceUserService.ListAllAsync(fsum);
                foreach (var item in listOfUsers)
                {
                    item.PrePath = "/" + _azureStorageOptions.Container;
                    var valueOfFavourite = listOfFavourite.Where(x => x.ServiceUserId == item.Id && x.UserId == userName).FirstOrDefault();
                    item.Favourite = valueOfFavourite == null ? false : true;
                }
            }
            var model = new HomeViewModel { ServiceUser = listOfUsers };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

