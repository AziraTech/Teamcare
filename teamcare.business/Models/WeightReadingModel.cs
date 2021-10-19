using System;
using System.Collections.Generic;
using System.Text;

namespace teamcare.business.Models
{
    public class WeightReadingModel : BaseModel
    {
        public Guid ServiceUserId { get; set; }
        public DateTime TestDate { get; set; }
        public decimal Weight { get; set; }
        public string WeightTestdate { get; set; }

    }
}
