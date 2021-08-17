using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.business.Services;
using teamcare.common.Configuration;
using teamcare.common.Enumerations;
using teamcare.common.ReferenceData;
using teamcare.web.app.ViewModels;

namespace teamcare.web.app.Controllers
{
	[Authorize]
	public class ServiceUsersController : BaseController
	{
		private readonly IServiceUserService _serviceUserService;
		private readonly IResidenceService _residenceService;
		private readonly IFileUploadService _fileUploadService;
        private readonly IDocumentUploadService _documentUploadService;
		private readonly AzureStorageSettings _azureStorageOptions;
		public ServiceUsersController(IServiceUserService serviceUserService, IResidenceService residenceService, IFileUploadService fileUploadService, IDocumentUploadService documentUploadService, IOptions<AzureStorageSettings> azureStorageOptions)
		{
			_serviceUserService = serviceUserService;
			_residenceService = residenceService;
			_fileUploadService = fileUploadService;
            _documentUploadService = documentUploadService;
			_azureStorageOptions = azureStorageOptions.Value;
		}

		public async Task<IActionResult> Index()
		{
			SetPageMetadata(PageTitles.ServiceUsers, SiteSection.ServiceUsers, new List<BreadcrumbItem>() {
				new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
				new BreadcrumbItem(PageTitles.ServiceUsers, string.Empty),
			});
			ViewBag.PrePath = "/" + _azureStorageOptions.Container;
			var listOfUser = await _serviceUserService.ListAllAsync();			
			listOfUser = _serviceUserService.ListAllSortedFiltered(0, null, (List<ServiceUserModel>)listOfUser);

			var  distinctArray = (from a in listOfUser select new { ServiceUserName = a.FirstName + " " + a.LastName, DateOfAdmission = a.DateOfAdmission }).ToArray()
								.Distinct().OrderBy(y => y.ServiceUserName).ToList();
			ViewBag.DistinctUserNames = new SelectList(distinctArray, "ServiceUserName", "ServiceUserName");

			var listOfResidence = await _residenceService.ListAllAsync();
			ViewBag.ListOfResidence = listOfResidence.ToArray();
			var distinctResidence = (from a in listOfResidence select new { ResidenceID = a.Id, ResidenceName = a.Name })
				.ToArray().Distinct().OrderBy(y => y.ResidenceName).ToList();
			ViewBag.DistinctResidence = new SelectList(distinctResidence, "ResidenceID", "ResidenceName");
			return View(listOfUser);
		}

		public async Task<IActionResult> Detail()
		{
			SetPageMetadata(PageTitles.ServiceUsers, SiteSection.ServiceUsers, new List<BreadcrumbItem>() {
				new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
				new BreadcrumbItem(PageTitles.ServiceUsers, Url.Action("Index", "ServiceUsers")),
				new BreadcrumbItem("Max Smith", null) //TODO: Replace with correct service user name
			});
			return View();
		}

		public async Task<IActionResult> SortFilterOption(int sortBy, string filterBy)
        {
			ViewBag.PrePath = "/" + _azureStorageOptions.Container;
			var listOfUser = await _serviceUserService.ListAllAsync();
			//Sorting List
			listOfUser = _serviceUserService.ListAllSortedFiltered(sortBy, filterBy, (List<ServiceUserModel>)listOfUser);
			//Distinct DropDown
			var distinctArray = (from a in listOfUser select new { ServiceUserName = a.FirstName + " " + a.LastName, DateOfAdmission = a.DateOfAdmission })
				.ToArray().Distinct().OrderBy(y => y.ServiceUserName).ToList();
			ViewBag.DistinctUserNames = new SelectList(distinctArray, "ServiceUserName", "ServiceUserName");			
			//Residence List
			var listOfResidence = await _residenceService.ListAllAsync();
			ViewBag.ListOfResidence = listOfResidence.ToArray();
			var distinctResidence = (from a in listOfResidence select new { ResidenceID = a.Id,  ResidenceName = a.Name })
				.ToArray().Distinct().OrderBy(y => y.ResidenceName).ToList();
			ViewBag.DistinctResidence = new SelectList(distinctResidence, "ResidenceID", "ResidenceName");

			return PartialView("_DataContent", listOfUser);
		}

		[HttpPost]
		public async Task<IActionResult> Save(ServiceUserCreateViewModel serviceUserCreateViewModel)
		{
			try
			{
				if (serviceUserCreateViewModel?.ServiceUser != null)
				{
					var createdServiceUser = await _serviceUserService.AddAsync(serviceUserCreateViewModel.ServiceUser);

					if (createdServiceUser != null && !string.IsNullOrWhiteSpace(serviceUserCreateViewModel.ServiceUser.TempFileId))
					{
						//	Get the temporary document
                        var document =
                            await _documentUploadService.GetByIdAsync(Guid.Parse(serviceUserCreateViewModel.ServiceUser.TempFileId));

                        var relocateFile = await _fileUploadService.MoveBlobAsync(new FileUploadModel
                        {
                            BlobName = document.BlobName,
							DestinationFolder = createdServiceUser.Id.ToString()
                        });

                        if (relocateFile != null)
                        {
                            document.DocumentType = (int)DocumentTypes.ProfilePhoto;
                            document.IsTemporary = false;
                            document.ServiceUserId = createdServiceUser.Id;
                            document.BlobName = relocateFile.BlobName;

                            await _documentUploadService.UpdateAsync(document);
                        }

                        var returnDoc = await _serviceUserService.GetByIdAsync(createdServiceUser.Id.Value);
                    }
				}
			}
			catch (Exception ex)
			{
			}
			return Json(1);
		}
	}
}
