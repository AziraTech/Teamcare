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
using teamcare.common.Helpers;
using teamcare.common.ReferenceData;
using teamcare.web.app.Helpers;
using teamcare.web.app.ViewModels;

namespace teamcare.web.app.Controllers
{
    [AuthorizeEnum(UserRoles.GlobalAdmin, UserRoles.Admin)]
    public class ServiceUsersController : BaseController
    {
        private readonly IServiceUserService _serviceUserService;
        private readonly IResidenceService _residenceService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IDocumentUploadService _documentUploadService;
        private readonly IFavouriteServiceUserService _favouriteServiceUserService;
        private readonly AzureStorageSettings _azureStorageOptions;
        private readonly IUserService _userService;
        public Guid userName;
        private readonly IContactService _contactService;

        public ServiceUsersController(IServiceUserService serviceUserService, 
                                      IResidenceService residenceService, 
                                      IUserService userService,
                                      IFileUploadService fileUploadService, 
                                      IDocumentUploadService documentUploadService,
                                      IFavouriteServiceUserService favouriteServiceUserService,
                                      IOptions<AzureStorageSettings> azureStorageOptions,
                                      IContactService contactService)
        {
            _serviceUserService = serviceUserService;
            _residenceService = residenceService;
            _fileUploadService = fileUploadService;
            _documentUploadService = documentUploadService;
            _favouriteServiceUserService = favouriteServiceUserService;
            _azureStorageOptions = azureStorageOptions.Value;
            _contactService = contactService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            SetPageMetadata(PageTitles.ServiceUsers, SiteSection.ServiceUsers, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.ServiceUsers, string.Empty),
            });

            var tempUser = User.FindFirstValue(common.ReferenceData.ClaimTypes.PreferredUsername);
            userName = await _userService.GetUserGuidAsync(tempUser);

            var listOfResidence = await _residenceService.ListAllAsync();
            var distinctResidence = listOfResidence.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).OrderBy(y => y.Text).ToList();

            var model = new ServiceUsersViewModel
            {                
                ResidenceList =distinctResidence,                
                CreateViewModel = new ServiceUserCreateViewModel
                {
                    Title = EnumExtensions.GetEnumListItems<NameTitle>(),
                    Marital=EnumExtensions.GetEnumListItems<MaritalStatus>(),
                    Religion = EnumExtensions.GetEnumListItems<Religion>(),
                    Ethnicity = EnumExtensions.GetEnumListItems<Ethnicity>(),
                    PrefLanguage = EnumExtensions.GetEnumListItems<Language>(),
                }
            };
            if (model.ServiceUser != null)
            {
                var listOfFavourite = await _favouriteServiceUserService.ListAllAsync();
                foreach (ServiceUserModel serviceUser in model.ServiceUser)
                {
                    var valueOfFavourite = listOfFavourite.Where(x => x.ServiceUserId == serviceUser.Id && x.UserId == userName).FirstOrDefault();
                    serviceUser.Favourite = valueOfFavourite == null ? false : true;
                }
            }
            return View(model);
        }
                
        public async Task<IActionResult> Detail(string id)
        {
            var tempUser = User.FindFirstValue(common.ReferenceData.ClaimTypes.PreferredUsername);
            userName = await _userService.GetUserGuidAsync(tempUser);

            var listOfUser = await _serviceUserService.GetByIdAsync(new Guid(id));
            if (listOfUser == null) { return View(new ServiceUsersViewModel()); }
            listOfUser.PrePath = "/" + _azureStorageOptions.Container;

            foreach (var item in listOfUser.Contacts)
            {
                item.PrePath = "/" + _azureStorageOptions.Container;
                item.Sequence = item.IsNextOfKin ? 1 : item.IsEmergencyContact ? 2 : 3;
            }


            SetPageMetadata(PageTitles.ServiceUsers, SiteSection.ServiceUsers, new List<BreadcrumbItem>() {
                new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
                new BreadcrumbItem(PageTitles.ServiceUsers, Url.Action("Index", "ServiceUsers")),
                new BreadcrumbItem(listOfUser.Title+" "+ listOfUser.FirstName+" "+listOfUser.LastName, null) //TODO: Replace with correct service user name
			});

            var listOfFavourite = await _favouriteServiceUserService.ListAllAsync();
            var valueOfFavourite = listOfFavourite.Where(x => x.ServiceUserId == new Guid(id)).FirstOrDefault();
            listOfUser.Favourite = valueOfFavourite == null ? false : true;            
            var listOfResidence = await _residenceService.ListAllAsync();
            var distinctResidence = listOfResidence.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).OrderBy(y => y.Text).ToList();

            var model = new ServiceUsersViewModel
            {
                ServiceUserByID = listOfUser,
                ResidenceList = distinctResidence,
                CreateViewModel = new ServiceUserCreateViewModel
                {
                    Title = EnumExtensions.GetEnumListItems<NameTitle>(),
                    Marital = EnumExtensions.GetEnumListItems<MaritalStatus>(),
                    Religion = EnumExtensions.GetEnumListItems<Religion>(),
                    Ethnicity = EnumExtensions.GetEnumListItems<Ethnicity>(),
                    PrefLanguage = EnumExtensions.GetEnumListItems<Language>(),
                    Relationship = EnumExtensions.GetEnumListItems<Relationship>(),

                },
                ContactList = listOfUser.Contacts.OrderBy(r => r.Sequence)


            };
            return View(model);
        }

        public async Task<IActionResult> SortFilterOption(int sortBy, string filterBy)
        {
            var tempUser = User.FindFirstValue(common.ReferenceData.ClaimTypes.PreferredUsername);
            userName = await _userService.GetUserGuidAsync(tempUser);

            //Sorting List
            var listOfUser = await _serviceUserService.ListAllSortedFiltered(sortBy, filterBy);
            if (listOfUser != null)
            {
                var listOfFavourite = await _favouriteServiceUserService.ListAllAsync();
                foreach (var item in listOfUser)
                {
                    item.PrePath = "/" + _azureStorageOptions.Container;
                    var valueOfFavourite = listOfFavourite.Where(x => x.ServiceUserId == item.Id && x.UserId == userName).FirstOrDefault();
                    item.Favourite = valueOfFavourite == null ? false : true;
                }
            }
            //Residence List
            var listOfResidence = await _residenceService.ListAllAsync();
            var distinctResidence = listOfResidence.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).OrderBy(y => y.Text).ToList();

            var model = new ServiceUsersViewModel
            {
                ServiceUser = listOfUser,
                ResidenceList = distinctResidence,
            };

            return PartialView("_DataContent", model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ServiceUserCreateViewModel serviceUserCreateViewModel)
        {
            try
            {
                if (serviceUserCreateViewModel?.ServiceUser != null)
                {
                    var createdServiceUser = new ServiceUserModel();

                    if (serviceUserCreateViewModel.ServiceUser.Id.ToString() == "")
                    {
                        createdServiceUser = await _serviceUserService.AddAsync(serviceUserCreateViewModel.ServiceUser);
                    }
                    else
                    {
                        createdServiceUser = await _serviceUserService.UpdateAsync(serviceUserCreateViewModel.ServiceUser);
                    }

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

        [HttpPost]
        public async Task<IActionResult> SetAsFavouriteUser(string FavauriteUser)
        {
            try
            {
                var tempUser = User.FindFirstValue(common.ReferenceData.ClaimTypes.PreferredUsername);
                userName = await _userService.GetUserGuidAsync(tempUser);
                if (userName != null && FavauriteUser.Trim() != "")
                {                    
                    var listOfFavourite = await _favouriteServiceUserService.ListAllAsync();                    
                    var valueOfFavourite = listOfFavourite.Where(x => x.ServiceUserId == new Guid(FavauriteUser) && x.UserId == userName).FirstOrDefault(); 
                    if (valueOfFavourite == null )
                    {
                        var createdFavouriteServiceUser = new FavouriteServiceUserModel();
                        createdFavouriteServiceUser.UserId = userName;
                        createdFavouriteServiceUser.ServiceUserId = new Guid(FavauriteUser);
                        createdFavouriteServiceUser = await _favouriteServiceUserService.AddAsync(createdFavouriteServiceUser);
                        return Json(true);
                    }
                    else
                    {
                        await _favouriteServiceUserService.DeleteAsync(valueOfFavourite);
                    }
                    
                }
            }
            catch (Exception ex)
            {
            }
            return Json(false);
        }

    }
}
