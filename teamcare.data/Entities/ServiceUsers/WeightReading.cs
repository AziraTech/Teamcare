using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace teamcare.data.Entities.ServiceUsers
{
    [Table("WeightReadings")]
    public class WeightReading : BaseEntity
    {
        [ForeignKey("ServiceUser")]
        [Column("service_user_id")]
        public Guid ServiceUserId { get; set; }
        public virtual ServiceUser ServiceUser { get; set; }

        [Column("test_date")]
        public DateTime TestDate { get; set; }

        [Column("Weight")]
        public decimal Weight { get; set; }
    }
}
