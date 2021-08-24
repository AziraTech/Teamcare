using System;
using System.Collections.Generic;
using System.Linq;
using teamcare.common.Enumerations;

namespace teamcare.business.Models
{
    public class ServiceUserModel : BaseModel
    {
        public NameTitle Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string KnownAs { get; set; }
        public string TempFileId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LegalStatus { get; set; }
        public string NHSIdNumber { get; set; }
        public string NationalInsuranceNo { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public Guid ResidenceId { get; set; }
        public string PersonalTelNo { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Religion Religion { get; set; }
        public Ethnicity Ethnicity { get; set; }
        public Language PreferredFirstLanguage { get; set; }
        public string CurrentPreviousOccupation { get; set; }
        public string NextOfKin { get; set; }
        public string RelationshipToPerson { get; set; }
        public string Address { get; set; }
        public string ContactDetails { get; set; }

        public ResidenceModel Residence { get; set; }
        public ICollection<DocumentUploadModel> DocumentUploads { get; set; }
        public DocumentUploadModel ProfilePhoto => DocumentUploads?.FirstOrDefault(i => i.IsTemporary == false && i.DocumentType == (int)DocumentTypes.ProfilePhoto);
        public string PrePath { get; set; }
    }
}
