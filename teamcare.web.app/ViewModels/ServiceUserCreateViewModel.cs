using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using teamcare.business.Models;
using teamcare.common.Configuration;
using teamcare.common.Models;

namespace teamcare.web.app.ViewModels
{
    public class ServiceUserCreateViewModel : BaseViewModel
    {
        public ServiceUserCreateViewModel()
        {

        }
        public ServiceUserCreateViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {

        }
        public ServiceUserModel ServiceUser { get; set; }
        public string TempFileId { get; set; }
        public IEnumerable<EnumListItem> Title { get; set; }
        public IEnumerable<EnumListItem> Marital { get; set; }
        public IEnumerable<EnumListItem> Religion { get; set; }
        public IEnumerable<EnumListItem> Ethnicity { get; set; }
        public IEnumerable<EnumListItem> PrefLanguage { get; set; }

    }
}