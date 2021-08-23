using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using teamcare.common.Configuration;

namespace teamcare.web.app.ViewModels
{
    public class BaseViewModel
    {
        private readonly AzureStorageSettings _azureStorageOptions;

        public BaseViewModel()
        {
            
        }

        public BaseViewModel(IOptions<AzureStorageSettings> azureStorageOptions)
        {
            _azureStorageOptions = azureStorageOptions.Value;
        }

        public string PrePath => $"/{_azureStorageOptions.Container}";
    }
}
