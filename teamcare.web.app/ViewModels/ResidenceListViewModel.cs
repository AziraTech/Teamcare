using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.common.Configuration;

namespace teamcare.web.app.ViewModels
{
    public class ResidenceListViewModel : BaseViewModel
    {
        public ResidenceListViewModel()
        {

        }
        public ResidenceListViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {

        }
        public IEnumerable<business.Models.ResidenceModel> Residences { get; set; }
        public business.Models.ResidenceModel Residence { get; set; }

        public ResidenceCreateViewModel CreateViewModel { get; set; }
        public SelectList DistinctUserNames { get; set; }
        public SelectList DistinctResidence { get; set; }        
    }
}
