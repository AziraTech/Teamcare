using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using teamcare.business.Models;
using teamcare.common.Configuration;

namespace teamcare.web.app.ViewModels
{
    public class ResidenceCreateViewModel : BaseViewModel
    {
        public ResidenceCreateViewModel()
        {

        }

        public ResidenceCreateViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {
            
        }
        public ResidenceModel Residence { get; set; }
        public string TempFileId { get; set; }
        
    }
}
