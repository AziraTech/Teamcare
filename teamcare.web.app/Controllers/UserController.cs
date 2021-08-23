﻿using Microsoft.AspNetCore.Authorization;
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
using teamcare.common.Helpers;
using teamcare.common.Models;
using teamcare.common.ReferenceData;
using teamcare.web.app.ViewModels;

namespace teamcare.web.app.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IDocumentUploadService _documentUploadService;
        private readonly AzureStorageSettings _azureStorageOptions;

        public UserController( IUserService userService, IFileUploadService fileUploadService, IDocumentUploadService documentUploadService, IOptions<AzureStorageSettings> azureStorageOptions)
        {
            _userService = userService;
            _fileUploadService = fileUploadService;
            _documentUploadService = documentUploadService;
            _azureStorageOptions = azureStorageOptions.Value;
        }

        public async Task<IActionResult> Index()
        {
            SetPageMetadata(PageTitles.User, SiteSection.Users, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.User, string.Empty),
            });

            ViewBag.PrePath = "/" + _azureStorageOptions.Container;

            var model = new UserListViewModel
            {
                Users = await _userService.ListAllAsync(),
                CreateViewModel = new UserCreateViewModel
                {
                    UserRoles = EnumExtensions.GetEnumListItems<UserRoles>()
                }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserCreateViewModel userCreateViewModel)
        {
            try
            {
                if (userCreateViewModel?.User != null)
                {
                    var listOfUser = await _userService.ListAllAsync();
                    // check IfEmail already exists
                    var user = listOfUser.FirstOrDefault(u => u.Email == userCreateViewModel.User.Email);
                    if (user != null)
                    {
                        return Json(2);
                    }

                    var createdUser = await _userService.AddAsync(userCreateViewModel.User);

                    if (createdUser != null && !string.IsNullOrWhiteSpace(userCreateViewModel.TempFileId))
                    {
                        //	Get the temporary document
                        var document =
                            await _documentUploadService.GetByIdAsync(Guid.Parse(userCreateViewModel.TempFileId));

                        var relocateFile = await _fileUploadService.MoveBlobAsync(new FileUploadModel
                        {
                            BlobName = document.BlobName,
                            DestinationFolder = createdUser.Id.ToString()
                        });

                        if (relocateFile != null)
                        {
                            document.DocumentType = (int)DocumentTypes.ProfilePhoto;
                            document.IsTemporary = false;
                            document.UserId = createdUser.Id;
                            document.BlobName = relocateFile.BlobName;

                            await _documentUploadService.UpdateAsync(document);
                        }

                        var returnDoc = await _userService.GetByIdAsync(createdUser.Id.Value);
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
