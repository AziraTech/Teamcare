using Microsoft.Extensions.Options;
using teamcare.common.Configuration;
using teamcare.common.Enumerations;

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
        public string ProfilePhoto { get; set; }
        public string UserRole { get; set; }
    }
}
