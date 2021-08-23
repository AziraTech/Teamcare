using Microsoft.Extensions.Options;
using teamcare.common.Configuration;

namespace teamcare.web.app.ViewModels
{
    public class UserProfile : BaseViewModel
    {
        public UserProfile()
        {
            
        }

        public UserProfile(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {
            
        }

        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
