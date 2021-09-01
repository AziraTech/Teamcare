using System.Collections.Generic;
using Microsoft.Extensions.Options;
using teamcare.common.Configuration;

namespace teamcare.web.app.ViewModels
{
    public class UserListViewModel : BaseViewModel
    {
        public UserListViewModel()
        {
            
        }
        public UserListViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {
            
        }
        public IEnumerable<business.Models.UserModel> Users { get; set; }
        public business.Models.UserModel User { get; set; }
        public UserCreateViewModel CreateViewModel { get; set; }
        public new string PrePath { get; set; }
    }
}
