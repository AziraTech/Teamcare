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
using teamcare.data.Entities;
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


        public ResidenceController(IServiceUserService serviceUserService, IResidenceService residenceService, IFileUploadService fileUploadService, IDocumentUploadService documentUploadService, IOptions<AzureStorageSettings> azureStorageOptions)
        {
            _serviceUserService = serviceUserService;
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
            ViewBag.PrePath = "/" + _azureStorageOptions.Container;
            var listOfResidence = await _residenceService.GetByIdAsync(Id);
            return View(listOfResidence);
        }

        public async Task<IActionResult> SetCurrentTab(string tabName, string Id)
        {            
            switch(tabName)
            {
                case "Overview": return await ResidenceDetail(new Guid(Id)); //return PartialView("_ResidenceUpdate", listOfUser);
                case "Service_User": return await ServiceUserDetails(Id); 
            }
            return null;
        }

        public async Task<IActionResult> ResidenceDetail(Guid Id)
        {
            ViewBag.PrePath = "/" + _azureStorageOptions.Container;
            var listOfResidence = await _residenceService.GetByIdAsync(Id);
            
            return PartialView("_ResidenceUpdate", listOfResidence);
        }

        public async Task<IActionResult> ServiceUserDetails(string Id)
        {
            ViewBag.PrePath = "/" + _azureStorageOptions.Container;            
            IEnumerable<ServiceUserModel> listOfUser = await _serviceUserService.ListAllSortedFiltered(0, null,0);
            
            listOfUser = listOfUser.Where(x => x.ResidenceId == new Guid(Id));
            var distinctArray = (from a in listOfUser select new { ServiceUserName = a.FirstName + " " + a.LastName, DateOfAdmission = a.DateOfAdmission }).ToArray().Distinct().OrderBy(y => y.ServiceUserName).ToList();
            ViewBag.DistinctUserNames = new SelectList(distinctArray, "ServiceUserName", "ServiceUserName");
            var listOfResidence = await _residenceService.ListAllAsync();
            ViewBag.ListOfResidence = listOfResidence.ToArray();            
            var distinctResidence = (from a in listOfResidence select new { ResidenceID = a.Id, ResidenceName = a.Name }).ToArray().Distinct().OrderBy(y => y.ResidenceName).ToList();
            ViewBag.DistinctResidence = new SelectList(distinctResidence, "ResidenceID", "ResidenceName");

            return PartialView("_ResidenceServiceUserCard", listOfUser);
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
            catch (Exception)
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
