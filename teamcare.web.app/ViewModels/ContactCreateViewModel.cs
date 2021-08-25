using System.Collections.Generic;
using Microsoft.Extensions.Options;
using teamcare.business.Models;
using teamcare.common.Configuration;
using teamcare.common.Models;

namespace teamcare.web.app.ViewModels
{
    public class ContactCreateViewModel : BaseViewModel
    {
        public ContactCreateViewModel()
        {
            
        }
        public ContactCreateViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {
            
        }
        public ContactModel Contact { get; set; }
        public string TempFileId { get; set; }
        public IEnumerable<EnumListItem> Relationship { get; set; }
    }
}
