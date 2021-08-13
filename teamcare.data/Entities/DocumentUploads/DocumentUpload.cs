using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace teamcare.data.Entities.Documents
{
    [Table("DocumentUploads")]
    public class DocumentUpload : BaseEntity
    {
        [ForeignKey("ServiceUser")]
        [Column("service_user_id")]
        public Guid? ServiceUserId { get; set; }
        public virtual ServiceUser ServiceUser { get; set; }

        [ForeignKey("Residence")]
        [Column("residence_id")]
        public Guid? ResidenceId { get; set; }
        public virtual Residence Residence { get; set; }

        [Column("file_extension")]
        public string FileExtension { get; set; }

        [Column("file_name")]
        public string FileName { get; set; }

        [Column("blob_name")]
        public string BlobName { get; set; }

        [Column("document_type")]
        public string DocumentType { get; set; }

        [Column("content_type")]
        public string ContentType { get; set; }

        [Column("document_subtype")]
        public string DocumentSubtype { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("is_temporary")]
        public bool IsTemporary { get; set; }

        [Column("file_size_in_bytes")]
        public long FileSizeInBytes { get; set; }
    }
}
