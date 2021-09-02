using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using teamcare.common.Configuration;

namespace teamcare.web.app.ViewModels
{
    public class ServiceUserLogViewModel : BaseViewModel
    {
        public ServiceUserLogViewModel()
        {

        }
        public ServiceUserLogViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {

        }

        public IEnumerable<business.Models.ServiceUserModel> ServiceUser { get; set; }
        public business.Models.ServiceUserModel ServiceUserByID { get; set; }

        public IEnumerable<business.Models.ServiceUserLogModel> ServiceUserLog { get; set; }
        public business.Models.ServiceUserLogModel ServiceUserLogByID { get; set; }
        public ServiceUserLogCreateViewModel CreateViewModel { get; set; }
        public List<SelectListItem> ServcieUsersList { get; set; }
    }
}
