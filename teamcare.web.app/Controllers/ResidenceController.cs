using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    public class ResidenceController : BaseController
    {
        private readonly IResidenceService _residenceService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IDocumentUploadService _documentUploadService;
        private readonly AzureStorageSettings _azureStorageOptions;


        public ResidenceController(IResidenceService residenceService, IFileUploadService fileUploadService, IDocumentUploadService documentUploadService, IOptions<AzureStorageSettings> azureStorageOptions)
        {
            _residenceService = residenceService;
            _fileUploadService = fileUploadService;
            _documentUploadService = documentUploadService;
            _azureStorageOptions = azureStorageOptions.Value;

        }

        public async Task<IActionResult> Index()
        {
            SetPageMetadata(PageTitles.Residence, SiteSection.Residence, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.Residence, string.Empty),
            });

            ViewBag.PrePath = "/" + _azureStorageOptions.Container;

            var listOfResidence = await _residenceService.ListAllAsync();
            return View(listOfResidence);

        }

        public async Task<IActionResult> Detail(Guid Id)
        {
          
            SetPageMetadata(PageTitles.Residence, SiteSection.Residence, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.Residence, Url.Action("Index", "Residence"))
			});
            var listOfResidence = await _residenceService.GetByIdAsync(Id);
            return View(listOfResidence);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ResidenceCreateViewModel residenceCreateViewModel)
        {
            try
            {
                if (residenceCreateViewModel?.Residence != null)
                {
                    var createdResidence = await _residenceService.AddAsync(residenceCreateViewModel.Residence);

                    if (createdResidence != null && !string.IsNullOrWhiteSpace(residenceCreateViewModel.TempFileId))
                    {
                        //	Get the temporary document
                        var document =
                            await _documentUploadService.GetByIdAsync(Guid.Parse(residenceCreateViewModel.TempFileId));

                        var relocateFile = await _fileUploadService.MoveBlobAsync(new FileUploadModel
                        {
                            BlobName = document.BlobName,
                            DestinationFolder = createdResidence.Id.ToString()
                        });

                        if (relocateFile != null)
                        {
                            document.DocumentType = (int)DocumentTypes.ProfilePhoto;
                            document.IsTemporary = false;
                            document.ResidenceId = createdResidence.Id;
                            document.BlobName = relocateFile.BlobName;

                            await _documentUploadService.UpdateAsync(document);
                        }

                        var returnDoc = await _residenceService.GetByIdAsync(createdResidence.Id.Value);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Json(1);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ResidenceCreateViewModel residenceCreateViewModel)
        {
            try
            {
                if (residenceCreateViewModel?.Residence != null)
                {
                    var createdResidence = await _residenceService.UpdateAsync(residenceCreateViewModel.Residence);

                    if (createdResidence != null && !string.IsNullOrWhiteSpace(residenceCreateViewModel.TempFileId))
                    {
                        //	Get the temporary document
                        var document =
                            await _documentUploadService.GetByIdAsync(Guid.Parse(residenceCreateViewModel.TempFileId));

                        var relocateFile = await _fileUploadService.MoveBlobAsync(new FileUploadModel
                        {
                            BlobName = document.BlobName,
                            DestinationFolder = createdResidence.Id.ToString()
                        });

                        if (relocateFile != null)
                        {
                            document.DocumentType = (int)DocumentTypes.ProfilePhoto;
                            document.IsTemporary = false;
                            document.ResidenceId = createdResidence.Id;
                            document.BlobName = relocateFile.BlobName;

                            await _documentUploadService.UpdateAsync(document);
                        }

                        var returnDoc = await _residenceService.GetByIdAsync(createdResidence.Id.Value);
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
