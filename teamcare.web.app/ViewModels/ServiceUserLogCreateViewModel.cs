using Microsoft.Extensions.Options;
using teamcare.business.Models;
using teamcare.common.Configuration;

namespace teamcare.web.app.ViewModels
{
     
    public class ServiceUserLogCreateViewModel : BaseViewModel
    {
        public ServiceUserLogCreateViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {

        }
        public ServiceUserLogModel ServiceUserLog { get; set; }
        public string TempFileId { get; set; }
    }
}
