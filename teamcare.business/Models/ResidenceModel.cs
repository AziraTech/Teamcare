using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using teamcare.common.Enumerations;

namespace teamcare.business.Models
{
    public class ResidenceModel : BaseModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Capacity { get; set; }
        public string Home_Tel_No { get; set; }
        public DateTime? ArchivedOn { get; set; }
        public ArchiveReasonResidence ArchivedReason { get; set; }

        public ICollection<ServiceUserModel> ServiceUsers { get; set; }

        public ICollection<DocumentUploadModel> DocumentUploads { get; set; }

        public DocumentUploadModel ProfilePhoto => DocumentUploads?.OrderByDescending(x => x.CreatedOn).FirstOrDefault(i => i.IsTemporary == false && i.DocumentType == (int)DocumentTypes.ProfilePhoto);

        public string PrePath { get; set; } 
    }
}
