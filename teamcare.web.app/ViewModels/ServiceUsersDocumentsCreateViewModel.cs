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
    public class ServiceUsersDocumentsCreateViewModel : BaseViewModel
    {
        public ServiceUsersDocumentsCreateViewModel()
        {

        }
        public ServiceUsersDocumentsCreateViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {

        }
        public ServiceUsersDocumentsModel ServiceUserDocument { get; set; }
        public string TempFileId { get; set; }
        public string SelectedDocumentCategory { get; set; } = "";
        public IEnumerable<EnumListItem> DocumentCategories { get; set; }
        public string PrePath { get; set; }

    }
}