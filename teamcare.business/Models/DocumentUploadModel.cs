﻿using System;
using System.Collections.Generic;
using System.Text;

namespace teamcare.business.Models
{
    public class DocumentUploadModel : BaseModel
    {
        public Guid? ServiceUserId { get; set; }

        public Guid? ResidenceId { get; set; }

        public Guid? UserId { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? ServiceUserDocumentId { get; set; }

        public string FileExtension { get; set; }

        public string BlobName { get; set; }
        public string FileName { get; set; }

		public int DocumentType { get; set; }
        public string ContentType { get; set; }

        public string DocumentSubtype { get; set; }

        public string Description { get; set; }

        public long FileSizeInBytes { get; set; }

        public bool IsTemporary { get; set; }
    }
}
