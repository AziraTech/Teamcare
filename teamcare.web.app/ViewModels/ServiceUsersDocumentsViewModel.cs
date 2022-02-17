using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.common.Configuration;

namespace teamcare.web.app.ViewModels
{     

    public class ServiceUsersDocumentsViewModel : BaseViewModel
    {
        public ServiceUsersDocumentsViewModel()
        {

        }
        public ServiceUsersDocumentsViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {

        }

        public ServiceUsersDocumentsCreateViewModel CreateViewModel { get; set; }
        public IEnumerable<business.Models.ServiceUsersDocumentsModel> ServiceUsersDocument { get; set; }
        public business.Models.ServiceUsersDocumentsModel ServiceUsersDocumentByID { get; set; }
        public IEnumerable<business.Models.ServiceUserModel> ServiceUser { get; set; }
        public business.Models.ServiceUserModel ServiceUserByID { get; set; }
        public Guid UserId { get; set; }

    }
}
