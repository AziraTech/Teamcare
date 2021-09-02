using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IServiceUserService _serviceUserService;
        private readonly IResidenceService _residenceService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IDocumentUploadService _documentUploadService;
        private readonly AzureStorageSettings _azureStorageOptions;
        private readonly IUserService _userService;
        public Guid userName;
        public ResidenceModel rm = new ResidenceModel();
        public DocumentUploadModel dum = new DocumentUploadModel();


        public ResidenceController(IServiceUserService serviceUserService, IResidenceService residenceService, IFileUploadService fileUploadService,
                                    IDocumentUploadService documentUploadService, IOptions<AzureStorageSettings> azureStorageOptions, IUserService userService)
        {
            _serviceUserService = serviceUserService;
            _residenceService = residenceService;
            _fileUploadService = fileUploadService;
            _documentUploadService = documentUploadService;
            _azureStorageOptions = azureStorageOptions.Value;
            _userService = userService;

        }

        public async Task<IActionResult> Index()
        {
            SetPageMetadata(PageTitles.Residence, SiteSection.Residence, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.Residence, string.Empty),
            });

            var listOfResidence = await _residenceService.ListAllAsync(rm);
            var ReturnResidenceModel = new ResidenceListViewModel
            {
                Residences = listOfResidence
            };
            foreach (var item in ReturnResidenceModel.Residences) { item.PrePath = "/" + _azureStorageOptions.Container; }
            return View(ReturnResidenceModel);

        }

        public async Task<IActionResult> Detail(Guid Id)
        {

            SetPageMetadata(PageTitles.Residence, SiteSection.Residence, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.Residence, Url.Action("Index", "Residence"))
            });
            rm.CreatedBy = userName;
            var listOfResidence = await _residenceService.GetByIdAsync(Id,rm);
            listOfResidence.PrePath = "/" + _azureStorageOptions.Container;
            return View(listOfResidence);            

        }

        public async Task<IActionResult> SetCurrentTab(string tabName, string Id)
        {
            switch (tabName)
            {
                case "Overview": return await ResidenceDetail(new Guid(Id)); 
                case "Service_User": return await ServiceUserDetails(Id);
            }
            return null;
        }

        public async Task<IActionResult> ResidenceDetail(Guid Id)
        {
            rm.CreatedBy = userName;

            var listOfResidence = await _residenceService.GetByIdAsync(Id,rm);        
            listOfResidence.PrePath = "/" + _azureStorageOptions.Container;
            return PartialView("_ResidenceUpdate", listOfResidence);
        }

        public async Task<IActionResult> ServiceUserDetails(string Id)
        {
            rm.CreatedBy = userName;

            ResidenceListViewModel ReturnResidenceModel = new ResidenceListViewModel();
            var listOfResidence = await _residenceService.GetByIdAsync(new Guid(Id),rm);
            if (listOfResidence != null)
            {
                listOfResidence.PrePath = "/" + _azureStorageOptions.Container;
                ReturnResidenceModel.Residence = listOfResidence;
                foreach (var item in ReturnResidenceModel.Residence.ServiceUsers) { item.PrePath = "/" + _azureStorageOptions.Container; }
            }
            return PartialView("_ResidenceServiceUserCard", ReturnResidenceModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ResidenceCreateViewModel residenceCreateViewModel)
        {
            try
            {
                if (residenceCreateViewModel?.Residence != null)
                {
                    rm.CreatedBy = userName;

                    var createdResidence = await _residenceService.AddAsync(residenceCreateViewModel.Residence);

                    if (createdResidence != null && !string.IsNullOrWhiteSpace(residenceCreateViewModel.TempFileId))
                    {
                        dum.CreatedBy = userName;
                        //	Get the temporary document
                        var document =
                            await _documentUploadService.GetByIdAsync(Guid.Parse(residenceCreateViewModel.TempFileId),dum);

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

                        var returnDoc = await _residenceService.GetByIdAsync(createdResidence.Id.Value,rm);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Json(1);
        }

        //[HttpPost]
        //public async Task<IActionResult> Update(ResidenceCreateViewModel residenceCreateViewModel)
        //{
        //    try
        //    {
        //        if (residenceCreateViewModel?.Residence != null)
        //        {
        //            var createdResidence = await _residenceService.UpdateAsync(residenceCreateViewModel.Residence);

        //            if (createdResidence != null && !string.IsNullOrWhiteSpace(residenceCreateViewModel.TempFileId))
        //            {
        //                //	Get the temporary document
        //                var document =
        //                    await _documentUploadService.GetByIdAsync(Guid.Parse(residenceCreateViewModel.TempFileId));

        //                var relocateFile = await _fileUploadService.MoveBlobAsync(new FileUploadModel
        //                {
        //                    BlobName = document.BlobName,
        //                    DestinationFolder = createdResidence.Id.ToString()
        //                });

        //                if (relocateFile != null)
        //                {
        //                    document.DocumentType = (int)DocumentTypes.ProfilePhoto;
        //                    document.IsTemporary = false;
        //                    document.ResidenceId = createdResidence.Id;
        //                    document.BlobName = relocateFile.BlobName;

        //                    await _documentUploadService.UpdateAsync(document);
        //                }

        //                var returnDoc = await _residenceService.GetByIdAsync(createdResidence.Id.Value);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return Json(1);
        //}
    }
}
