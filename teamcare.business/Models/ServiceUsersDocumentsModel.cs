using System;
using System.Collections.Generic;
using System.Linq;
using teamcare.common.Enumerations;

namespace teamcare.business.Models
{
    public class ServiceUsersDocumentsModel : BaseModel
    {
        public Guid ServiceUserId { get; set; }     
        public DateTime DateReceived { get; set; } = DateTime.UtcNow;
        public string Title { get; set; }
        public string Description { get; set; }
        public DocumentTypes DocumentCategory { get; set; }
        public ICollection<DocumentUploadModel> DocumentUploads { get; set; }
        public DocumentUploadModel DocumentFile => DocumentUploads?.OrderByDescending(x => x.CreatedOn).FirstOrDefault(i => i.ServiceUserDocumentId == Id && i.IsTemporary == false && i.DocumentType != (int)DocumentTypes.ProfilePhoto);
        public string PrePath { get; set; }

    }
}
