using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using teamcare.business.Models;
using teamcare.common.Configuration;
using teamcare.common.Models;

namespace teamcare.web.app.ViewModels
{
    public class AuditCreateViewModel : BaseViewModel
    {
        public AuditCreateViewModel()
        {

        }
        public AuditCreateViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {

        }
        public AuditModel Audit { get; set; }
       

    }
}