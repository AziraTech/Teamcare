using System;

namespace teamcare.business.Models
{
    public class ServiceUserModel : BaseModel
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string KnownAs { get; set; }
        public string TempFileId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LegalStatus { get; set; }
        public string NHSIdNumber { get; set; }
        public string NationalInsuranceNo { get; set; }
        public string DateOfAdmission { get; set; }
        public Guid ResidenceId { get; set; }
        public string PersonalTelNo { get; set; }
        public string MaritalStatus { get; set; }
        public string Religion { get; set; }
        public string Ethnicity { get; set; }
        public string PreferredFirstLanguage { get; set; }
        public string CurrentPreviousOccupation { get; set; }
        public string NextOfKin { get; set; }
        public string RelationshipToPerson { get; set; }
        public string Address { get; set; }
        public string ContactDetails { get; set; }
    }
}
