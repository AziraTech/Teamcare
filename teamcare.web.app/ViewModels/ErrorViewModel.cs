using Microsoft.Extensions.Options;
using teamcare.common.Configuration;

namespace teamcare.web.app.ViewModels
{
    public class ErrorViewModel : BaseViewModel
    {
        public ErrorViewModel()
        {
            
        }
        public ErrorViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {
            
        }

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
