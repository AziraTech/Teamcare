using System.Collections.Generic;
using Microsoft.Extensions.Options;
using teamcare.business.Models;
using teamcare.common.Configuration;
using teamcare.common.Models;
using teamcare.data.Entities.ServiceUsers;

namespace teamcare.web.app.ViewModels
{
    public class HealthMedicationViewModel : BaseViewModel
    {
        public HealthMedicationViewModel() : base()
        {

        }
        public HealthMedicationModel HealthMedication { get; set; }
        public IEnumerable<business.Models.HealthMedicationModel> HealthMedicationList { get; set; }
        public BloodPressureReadingModel BloodPressureReading { get;set;}
    }
}
