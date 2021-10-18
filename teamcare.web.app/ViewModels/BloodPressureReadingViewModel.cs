using System.Collections.Generic;
using Microsoft.Extensions.Options;
using teamcare.business.Models;
using teamcare.common.Configuration;
using teamcare.common.Models;
using teamcare.data.Entities.ServiceUsers;

namespace teamcare.web.app.ViewModels
{
    public class BloodPressureReadingViewModel : BaseViewModel
    {
        public BloodPressureReadingViewModel() : base()
        {
            
        }
        public BloodPressureReadingModel BloodPressureReading { get; set; }
        public IEnumerable<business.Models.BloodPressureReadingModel> BloodPressureReadingList { get; set; }
    }
}
