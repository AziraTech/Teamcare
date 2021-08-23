using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using teamcare.business.Models;
using teamcare.common.Configuration;

namespace teamcare.web.app.ViewModels
{
    public class ServiceUserCreateViewModel : BaseViewModel
    {
        public ServiceUserCreateViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {
            
        }
        public ServiceUserModel ServiceUser { get; set; }
    }
}
