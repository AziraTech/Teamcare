using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using teamcare.common.Configuration;

namespace teamcare.web.app.ViewModels
{
    public class AuditViewModel : BaseViewModel
    {
        public AuditViewModel()
        {

        }
        public AuditViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {

        }
        public IEnumerable<business.Models.ServiceUserModel> ServiceUser { get; set; }
        public IEnumerable<business.Models.AuditModel> Audit { get; set; }
        public business.Models.AuditModel AuditByID { get; set; }
        public AuditCreateViewModel CreateViewModel { get; set; }
        public List<SelectListItem> ServcieUsersList { get; set; }
        public Guid UserId { get; set; }
    }
}
