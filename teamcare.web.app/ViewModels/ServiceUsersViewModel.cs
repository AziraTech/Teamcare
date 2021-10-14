﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using teamcare.common.Configuration;
using teamcare.common.Models;

namespace teamcare.web.app.ViewModels
{
    public class ServiceUsersViewModel : BaseViewModel
    {
        public ServiceUsersViewModel()
        {

        }
        public ServiceUsersViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {

        }
        public IEnumerable<business.Models.ServiceUserModel> ServiceUser { get; set; }
        public business.Models.ServiceUserModel ServiceUserByID { get; set; }
        public ServiceUserCreateViewModel CreateViewModel { get; set; }
        public List<SelectListItem> ResidenceList { get; set; }
        public IEnumerable<business.Models.ContactModel> ContactList { get; set; }             
        public IEnumerable<business.Models.AssessmentTypeModel> AssessmentType { get; set; }
        public IEnumerable<business.Models.AssessmentSkillModel> AssessmentSkills { get; set; }
        public bool IsNextOfKin { get; set; }
        public bool IsNoEmergency { get; set; }

    }
}
