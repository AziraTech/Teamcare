using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace teamcare.data.Entities.ServiceUsers
{
    [Table("HealthMedications")]
    public class HealthMedication : BaseEntity
    {
        [ForeignKey("ServiceUser")]
        [Column("service_user_id")]
        public Guid ServiceUserId { get; set; }
        public virtual ServiceUser ServiceUser { get; set; }

        [Column("diagnosis")]
        public String Diagnosis { get; set; }

        [Column("prescription_history")]
        public String PrescriptionHistory { get; set; }

        [Column("social_history")]
        public String SocialHistory { get; set; }

        [Column("allergy_information")]
        public String AllergyInformation { get; set; }

        [Column("other_significant_information")]
        public String OtherSignificantInformation { get; set; }
    }
}
