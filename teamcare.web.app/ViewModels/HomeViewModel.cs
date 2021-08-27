
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using teamcare.business.Models;
using teamcare.common.Configuration;
using teamcare.common.Models;

namespace teamcare.web.app.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel() { }
        public HomeViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions) { }
        public IEnumerable<ServiceUserModel> ServiceUser { get; set; }
        public ServiceUserModel ServiceUserByID { get; set; }
        public ServiceUserCreateViewModel CreateViewModel { get; set; }
        public List<SelectListItem> ResidenceList { get; set; }

    }
}
