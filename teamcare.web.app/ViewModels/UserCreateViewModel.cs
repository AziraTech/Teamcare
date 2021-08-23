using System.Collections.Generic;
using Microsoft.Extensions.Options;
using teamcare.business.Models;
using teamcare.common.Configuration;
using teamcare.common.Models;

namespace teamcare.web.app.ViewModels
{
    public class UserCreateViewModel : BaseViewModel
    {
        public UserCreateViewModel()
        {
            
        }
        public UserCreateViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {
            
        }
        public UserModel User { get; set; }
        public string TempFileId { get; set; }
        public IEnumerable<EnumListItem> UserRoles { get; set; }
    }
}
