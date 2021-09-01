﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public Guid userName;
        public UserModel um = new UserModel();
        public DocumentUploadModel dum = new DocumentUploadModel();

        public UserController( IUserService userService, IFileUploadService fileUploadService, IDocumentUploadService documentUploadService, IOptions<AzureStorageSettings> azureStorageOptions)
        {
            _userService = userService;
            _fileUploadService = fileUploadService;
            _documentUploadService = documentUploadService;
            _azureStorageOptions = azureStorageOptions.Value;
        }

        public async Task<IActionResult> Index()
        {
            SetPageMetadata(PageTitles.User, SiteSection.Users, new List<BreadcrumbItem>() 
            {                
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.User, string.Empty),
            });

            var tempUser = User.FindFirstValue(common.ReferenceData.ClaimTypes.PreferredUsername);
            userName = await _userService.GetUserGuidAsync(tempUser);
            um.CreatedBy = userName;

            var usersDetail = await _userService.ListAllAsync(um);
            foreach (var item in usersDetail) { item.PrePath = "/" + _azureStorageOptions.Container; }
            var model = new UserListViewModel
            {
                Users = usersDetail,
                PrePath = "/" + _azureStorageOptions.Container,
                CreateViewModel = new UserCreateViewModel
                {
                    UserRoles = EnumExtensions.GetEnumListItems<UserRoles>()
                }
            };

            return View(model);
        }

        public async Task<IActionResult> Detail(Guid Id)
        {
            SetPageMetadata(PageTitles.User, SiteSection.Users, new List<BreadcrumbItem>() 
            {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.User, Url.Action("Index", "User"))
            });
            um.CreatedBy = userName;

            var listOfUser = await _userService.GetByIdAsync(Id,um);
            listOfUser.PrePath = "/" + _azureStorageOptions.Container;
            var model = new UserListViewModel
            {
                User = listOfUser,
                PrePath = "/" + _azureStorageOptions.Container,
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
                    um.CreatedBy = userName;

                    var listOfUser = await _userService.ListAllAsync(um);
                    // check IfEmail already exists
                    var user = listOfUser.FirstOrDefault(u => u.Email == userCreateViewModel.User.Email);
                    if (user != null)
                    {
                        return Json(2);
                    }

                    var createdUser = await _userService.AddAsync(userCreateViewModel.User);

                    if (createdUser != null && !string.IsNullOrWhiteSpace(userCreateViewModel.TempFileId))
                    {
                        dum.CreatedBy = userName;

                        //	Get the temporary document
                        var document =
                            await _documentUploadService.GetByIdAsync(Guid.Parse(userCreateViewModel.TempFileId),dum);

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

                        var returnDoc = await _userService.GetByIdAsync(createdUser.Id.Value,um);
                    }
                }
            }
            catch
            {
            }
            return Json(1);
        }
    }
}
