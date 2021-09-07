using System;
using System.Collections.Generic;
using System.Linq;
using teamcare.common.Enumerations;

namespace teamcare.business.Models
{
    public class ContactModel : BaseModel
    {
        public Guid ServiceUserId { get; set; }
        public NameTitle Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public Relationship Relationship { get; set; }
        public bool IsNextOfKin { get; set; }
        public bool IsEmergencyContact { get; set; }
        public ICollection<DocumentUploadModel> DocumentUploads { get; set; }
        public DocumentUploadModel ProfilePhoto => DocumentUploads?.FirstOrDefault(i => i.IsTemporary == false && i.DocumentType == (int)DocumentTypes.ProfilePhoto);
        public string PrePath { get; set; }
        public int Sequence { get; set; }
    }
}
