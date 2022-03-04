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
using teamcare.business.Models;

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

			if (User.Identity.IsAuthenticated)
			{
				if (UserRole == UserRoles.ServiceUser.ToString())
				{
					return Redirect("/ServiceUsers/Detail/" + ServiceUserId);
				}
			}
			var listOfUsers = await _serviceUserService.ListAllSortedFiltered(0, null, false);
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

			var listOfLog = await _serviceUserLogService.ListAllSortedFiltered(null, false, null,null);

			var audit = await _auditService.ListAllAsync();
			var myActivity = audit.Where(r => r.CreatedBy == base.UserId).ToList();

			var model = new HomeViewModel
			{
				ServiceUser = listOfUsers,
				MyAudit = myActivity
			};

			_auditService.Execute(async repository =>
			{
				await repository.CreateAuditRecord(new Audit { Action = AuditAction.HomePage, Details = this.UserName + " has accessed Home page.", UserReference = "", CreatedBy = base.UserId });
			});
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> ResetDashboardFavoriteUser()
		{
			try
			{
				var model = await _serviceUserService.ListAllAsync();
				var listOfFavourite = await _favouriteServiceUserService.ListAllAsync();
				foreach (ServiceUserModel serviceUser in model)
				{
					var valueOfFavourite = listOfFavourite.Where(x => x.ServiceUserId == serviceUser.Id && x.UserId == (Guid)base.UserId).FirstOrDefault();
					serviceUser.Favourite = valueOfFavourite == null ? false : true;
					serviceUser.PrePath = "/" + _azureStorageOptions.Container;
				}
				return PartialView("_HomeServiceUsersListItem", model);

			}
			catch (Exception ex)
			{

			}
			return null;
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

