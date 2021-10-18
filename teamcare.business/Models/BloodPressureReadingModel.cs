using System;
using System.Collections.Generic;
using System.Text;

namespace teamcare.business.Models
{
    public class BloodPressureReadingModel: BaseModel
    {
        public Guid ServiceUserId { get; set; }
        public DateTimeOffset TestDate { get; set; }
        public int SystolicReading { get; set; }
        public int DiastolicReading { get; set; }
        public int Pulse { get; set; }

    }
}
