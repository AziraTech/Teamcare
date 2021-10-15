using System;
using System.Collections.Generic;
using System.Text;

namespace teamcare.business.Models
{
    public class HealthMedicationModel: BaseModel
    {
        public Guid ServiceUserId { get; set; }
        public String Diagnosis { get; set; }
        public String PrescriptionHistory { get; set; }
        public String SocialHistory { get; set; }
        public String AllergyInformation { get; set; }
        public String OtherSignificantInformation { get; set; }

    }
}
