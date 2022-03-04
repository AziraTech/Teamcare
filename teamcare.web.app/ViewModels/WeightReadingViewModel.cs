using System.Collections.Generic;
using Microsoft.Extensions.Options;
using teamcare.business.Models;
using teamcare.common.Configuration;
using teamcare.common.Models;
using teamcare.data.Entities.ServiceUsers;

namespace teamcare.web.app.ViewModels
{
    public class WeightReadingViewModel : BaseViewModel
    {
        public WeightReadingViewModel() : base()
        {
            
        }
        public WeightReadingModel WeightReading { get; set; }
        public IEnumerable<business.Models.WeightReadingModel> WeightReadingList { get; set; }
        public string NotifyStatus { get; set; }
        public int DueDays { get; set; }
    }
}
