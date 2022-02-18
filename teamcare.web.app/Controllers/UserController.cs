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
using teamcare.common.Helpers;
using teamcare.common.ReferenceData;
using teamcare.data.Entities;
using teamcare.web.app.ViewModels;

namespace teamcare.web.app.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IServiceUserService _serviceUserService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IDocumentUploadService _documentUploadService;
        private readonly AzureStorageSettings _azureStorageOptions;
        private readonly IAuditService _auditService;
        public Guid userName;

        public UserController(IUserService userService, IFileUploadService fileUploadService, IDocumentUploadService documentUploadService, IOptions<AzureStorageSettings> azureStorageOptions, IAuditService auditService, IServiceUserService serviceUserService)
        {
            _userService = userService;
            _fileUploadService = fileUploadService;
            _documentUploadService = documentUploadService;
            _azureStorageOptions = azureStorageOptions.Value;
            _auditService = auditService;
            _serviceUserService = serviceUserService;
        }

        public async Task<IActionResult> Index()
        {
            SetPageMetadata(PageTitles.User, SiteSection.Users, new List<BreadcrumbItem>()
            {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.User, string.Empty),
            });

            var usersDetail = await _userService.ListAllAsync();
            foreach (var item in usersDetail) { item.PrePath = "/" + _azureStorageOptions.Container; }


            var model = new UserListViewModel
            {
                Users = usersDetail,
                PrePath = "/" + _azureStorageOptions.Container,
                CreateViewModel = new UserCreateViewModel
                {
                    UserRoles = EnumExtensions.GetEnumListItems<UserRoles>(),
                    Title = EnumExtensions.GetEnumListItems<NameTitle>(),
                    ServiceUserList = await ServiceUserList()
                }
            };

            _auditService.Execute(async repository =>
            {
                await repository.CreateAuditRecord(new Audit { Action = AuditAction.View, Details = "All User List.", UserReference = "", CreatedBy = base.UserId, CreatedOn = DateTimeOffset.UtcNow });
            });
            return View(model);
        }

        public async Task<IActionResult> Detail(Guid Id)
        {
            SetPageMetadata(PageTitles.User, SiteSection.Users, new List<BreadcrumbItem>()
            {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.User, Url.Action("Index", "User"))
            });

            var listOfUser = await _userService.GetByIdAsync(Id);
            listOfUser.PrePath = "/" + _azureStorageOptions.Container;



            var model = new UserListViewModel
            {
                User = listOfUser,
                PrePath = "/" + _azureStorageOptions.Container,
                CreateViewModel = new UserCreateViewModel
                {
                    UserRoles = EnumExtensions.GetEnumListItems<UserRoles>(),
                    Title = EnumExtensions.GetEnumListItems<NameTitle>(),
                    ServiceUserList = await ServiceUserList()

                }
            };
            return View(model);

        }

        public async Task<List<SelectListItem>> ServiceUserList()
        {
            var listOfServiceuser = await _serviceUserService.ListAllAsync();
            return listOfServiceuser.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.FirstName + " " + x.LastName
            }).OrderBy(y => y.Text).ToList();

        }

        [HttpPost]
        public async Task<IActionResult> Save(UserCreateViewModel userCreateViewModel)
        {
            try
            {
                if (userCreateViewModel?.User != null)
                {
                    var createdUser = new UserModel();

                    if (userCreateViewModel.User.Id.ToString() == "")
                    {
                        var listOfUser = await _userService.ListAllAsync();
                        // check IfEmail already exists
                        var user = listOfUser.FirstOrDefault(u => u.Email == userCreateViewModel.User.Email);
                        if (user != null)
                        {
                            return Json(new { statuscode = 2 });
                        }

                        createdUser = await _userService.AddAsync(userCreateViewModel.User);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = userCreateViewModel.User.FirstName + " " + userCreateViewModel.User.LastName + " has been created.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }
                    else
                    {
                        userCreateViewModel.User.ServiceUserId = userCreateViewModel.User.UserRole == UserRoles.ServiceUser ? userCreateViewModel.User.ServiceUserId : null;
                        createdUser = await _userService.UpdateAsync(userCreateViewModel.User);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = userCreateViewModel.User.FirstName + " " + userCreateViewModel.User.LastName + " has been updated.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }


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
                return Json(new { statuscode = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }


        public async Task<IActionResult> MyProfile()
        {
            SetPageMetadata(PageTitles.User, SiteSection.Users, new List<BreadcrumbItem>()
            {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.MyProfile,"0"),
            });

            var listOfUser = await _userService.GetByIdAsync((Guid)base.UserId);
            listOfUser.PrePath = "/" + _azureStorageOptions.Container;
            var model = new UserListViewModel
            {
                User = listOfUser,
                PrePath = "/" + _azureStorageOptions.Container,
                CreateViewModel = new UserCreateViewModel
                {
                    UserRoles = EnumExtensions.GetEnumListItems<UserRoles>(),
                    Title = EnumExtensions.GetEnumListItems<NameTitle>(),

                }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> MyProfileUpdate(UserCreateViewModel userCreateViewModel)
        {
            try
            {
                if (userCreateViewModel?.User != null)
                {
                    var createdUser = new UserModel();

                    var userdata = await _userService.GetByIdAsync((Guid)userCreateViewModel.User.Id);

                    if (userdata != null)
                    {
                        userdata.Title = userCreateViewModel.User.Title;
                        userdata.FirstName = userCreateViewModel.User.FirstName;
                        userdata.LastName = userCreateViewModel.User.LastName;
                        userdata.Position = userCreateViewModel.User.Position;
                        userdata.Phone_No = userCreateViewModel.User.Phone_No;
                        userdata.Mobile_No = userCreateViewModel.User.Mobile_No;
                        userdata.Address = userCreateViewModel.User.Address?.Trim();

                        createdUser = await _userService.UpdateAsync(userdata);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = userCreateViewModel.User.FirstName + " " + userCreateViewModel.User.LastName + " has been updated.", UserReference = "", CreatedBy = base.UserId });
                        });

                    }


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
                return Json(new { statuscode = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }
    }
}
