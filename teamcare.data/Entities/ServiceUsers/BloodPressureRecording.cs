using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace teamcare.data.Entities.ServiceUsers
{
    [Table("BloodPressureReadings")]
    public class BloodPressureReading : BaseEntity
    {
        [ForeignKey("ServiceUser")]
        [Column("service_user_id")]
        public Guid ServiceUserId { get; set; }
        public virtual ServiceUser ServiceUser { get; set; }

        [Column("test_date")]
        public DateTime TestDate { get; set; } = DateTime.UtcNow;

        [Column("systolic_reading")]
        public int SystolicReading { get; set; }

        [Column("diastolic_reading")]
        public int DiastolicReading { get; set; }

        [Column("pulse")]
        public int Pulse { get; set; }       
    }
}
