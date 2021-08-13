using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.business.Services;
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

        public ServiceUsersController(IServiceUserService serviceUserService, IResidenceService residenceService, IFileUploadService fileUploadService, IDocumentUploadService documentUploadService)
		{
			_serviceUserService = serviceUserService;
			_residenceService = residenceService;
			_fileUploadService = fileUploadService;
            _documentUploadService = documentUploadService;
        }

		public async Task<IActionResult> Index()
		{
			SetPageMetadata(PageTitles.ServiceUsers, SiteSection.ServiceUsers, new List<BreadcrumbItem>() {
				new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
				new BreadcrumbItem(PageTitles.ServiceUsers, string.Empty),
			});
			var listOfUser = await _serviceUserService.ListAllAsync();
			var listOfResidence = await _residenceService.ListAllAsync();
			ViewBag.ListOfResidence = listOfResidence.ToArray();
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
                            document.DocumentType = DocumentTypes.ProfilePhoto.ToString();
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

		public async Task<FileContentResult> SomeImage(string id)
		{
			var docList = await _documentUploadService.ListAllAsync();
			var imageByte = docList.ToArray().Where(x => x.ServiceUserId == new Guid(id)).ToArray()[0];
			var relocateFile = await _fileUploadService.GetBlobAsync(new FileUploadModel
			{
				BlobName = imageByte.BlobName,
				DestinationFolder = id,				
				FileName = "Test.jpg"
			});
			byte[] bytes = relocateFile; // imageByte. DocumentBytes;
			return File(bytes, "image/*");
		}

	}
}
