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
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IDocumentUploadService _documentUploadService;
        private readonly AzureStorageSettings _azureStorageOptions;
        private readonly IUserService _userService;
        public Guid userName;
        public ContactModel cm = new ContactModel();
        public DocumentUploadModel dum = new DocumentUploadModel();

        public ContactController(IContactService contactService, IFileUploadService fileUploadService, IDocumentUploadService documentUploadService, IOptions<AzureStorageSettings> azureStorageOptions, IUserService userService)
        {
            _contactService = contactService;
            _fileUploadService = fileUploadService;
            _documentUploadService = documentUploadService;
            _azureStorageOptions = azureStorageOptions.Value;
            _userService = userService;

        }

        public async Task<IActionResult> Index()
        {
            var tempUser = User.FindFirstValue(common.ReferenceData.ClaimTypes.PreferredUsername);
            userName = await _userService.GetUserGuidAsync(tempUser);

            cm.CreatedBy = userName;
            //SetPageMetadata(PageTitles.Contact, SiteSection.Contacts, new List<BreadcrumbItem>() 
            //{                
            //    new BreadcrumbItem(PageTitles.Dashboard, Url.Action("Index", "Home")),
            //    new BreadcrumbItem(PageTitles.Contact, string.Empty),
            //});

            //var contactsDetail = await _contactService.ListAllAsync();
            //foreach (var item in contactsDetail) { item.PrePath = "/" + _azureStorageOptions.Container; }
            //var model = new ContactListViewModel
            //{
            //    Contacts = contactsDetail,
            //    PrePath = "/" + _azureStorageOptions.Container,
            //    CreateViewModel = new ContactCreateViewModel
            //    {
            //        ContactRoles = EnumExtensions.GetEnumListItems<ContactRoles>()
            //    }
            //};

            return View();
        }

        public async Task<IActionResult> Detail(string id)
        {
            cm.CreatedBy = userName;
            var contactData = await _contactService.GetByIdAsync(new Guid(id), cm);
            contactData.PrePath = "/" + _azureStorageOptions.Container;
            return Json(contactData);
        }


        [HttpPost]
        public async Task<IActionResult> Save(ContactCreateViewModel contactCreateViewModel)
        {
            try
            {
                if (contactCreateViewModel?.Contact != null)
                {
                    cm.CreatedBy = userName;

                    var createdContact = new ContactModel();

                    if (contactCreateViewModel.Contact.Id.ToString() == "")
                    {
                        var listOfContact = await _contactService.ListAllAsync(cm);
                        // check IfEmail already exists
                        var contact = listOfContact.FirstOrDefault(u => u.Email == contactCreateViewModel.Contact.Email);
                        if (contact != null)
                        {
                            return Json(2);
                        }


                        createdContact = await _contactService.AddAsync(contactCreateViewModel.Contact);
                    }
                    else
                    {
                        createdContact = await _contactService.UpdateAsync(contactCreateViewModel.Contact);
                    }

                    if (createdContact != null && !string.IsNullOrWhiteSpace(contactCreateViewModel.TempFileId))
                    {
                        dum.CreatedBy = userName;
                        //	Get the temporary document
                        var document =
                            await _documentUploadService.GetByIdAsync(Guid.Parse(contactCreateViewModel.TempFileId), dum);

                        var relocateFile = await _fileUploadService.MoveBlobAsync(new FileUploadModel
                        {
                            BlobName = document.BlobName,
                            DestinationFolder = createdContact.Id.ToString()
                        });

                        if (relocateFile != null)
                        {
                            document.DocumentType = (int)DocumentTypes.ProfilePhoto;
                            document.IsTemporary = false;
                            document.ContactId = createdContact.Id;
                            document.BlobName = relocateFile.BlobName;

                            await _documentUploadService.UpdateAsync(document);
                        }

                        var returnDoc = await _contactService.GetByIdAsync(createdContact.Id.Value, cm);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Json(1);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                cm.Id = id;
                cm.CreatedBy = userName;
                await _contactService.DeleteAsync(cm);

                return Json(1);
            }
            catch (Exception ex)
            {

            }
            return Json(1);
        }
    }
}
