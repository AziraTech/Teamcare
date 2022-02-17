using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using teamcare.common.Enumerations;
using teamcare.data.Entities.Documents;

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
        public DateTime DateReceived { get; set; } = DateTime.UtcNow;

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("document_category")]
        public DocumentTypes DocumentCategory { get; set; }

        [Column("is_confidential")]
		public bool IsConfidential { get; set; }

		public virtual ICollection<DocumentUpload> DocumentUploads { get; set; }

    }
}
