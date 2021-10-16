using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using teamcare.common.Enumerations;

namespace teamcare.data.Entities.ServiceUsers
{
    [Table("ServiceUserDocuments")]
    public class ServiceUserDocument : BaseEntity
    {
        [ForeignKey("ServiceUser")]
        [Column("service_user_id")]
        public Guid ServiceUserId { get; set; }
        public virtual ServiceUser ServiceUser { get; set; }

        [Column("date_received")]
        public DateTimeOffset DateReceived { get; set; } = DateTimeOffset.UtcNow;

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("document_category")]
        public DocumentCategories DocumentCategory { get; set; }

    }
}
