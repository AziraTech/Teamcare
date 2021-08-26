using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using teamcare.common.Enumerations;
using teamcare.data.Entities;
using teamcare.data.Entities.Documents;
using teamcare.data.Entities.ServiceUsers;

namespace teamcare.data.Entities
{	
	[Table("ServiceUsers")]
	public class ServiceUser : BaseEntity
	{
		[Column("title")]
		public NameTitle Title { get; set; }

		[Column("first_name")]
		public string FirstName { get; set; }

		[Column("last_name")]
		public string LastName { get; set; }

		[Column("known_as")]
		public string KnownAs { get; set; }

		[Column("date_of_birth")]
		public DateTime DateOfBirth { get; set; }

		[Column("legal_status")]
		public string LegalStatus { get; set; }

		[Column("nhs_id_number")]
		public string NHSIdNumber { get; set; }

		[Column("national_insurance_no")]
		public string NationalInsuranceNo { get; set; }

		[Column("date_of_admission")]
		public DateTime DateOfAdmission { get; set; }

		[Column("residence_id")]
		public Guid ResidenceId { get; set; }

		[Column("personal_tel_no")]
		public string PersonalTelNo { get; set; }

		[Column("marital_status")]
		public MaritalStatus MaritalStatus { get; set; }

		[Column("religion")]
		public Religion Religion { get; set; }

		[Column("ethnicity")]
		public Ethnicity Ethnicity { get; set; }

		[Column("preferred_first_language")]
		public Language PreferredFirstLanguage { get; set; }

		[Column("current_previous_occupation")]
		public string CurrentPreviousOccupation { get; set; }

		[Column("next_of_kin")]
		public string NextOfKin { get; set; }

		[Column("relationship_to_person")]
		public string RelationshipToPerson { get; set; }

		[Column("address")]
		public string Address { get; set; }

		[Column("contact_details")]
		public string ContactDetails { get; set; }

		[Column("archived_on")]
		public DateTime? ArchivedOn { get; set; }

		[Column("archived_reason")]
		public string ArchivedReason { get; set; }

		public virtual ICollection<DocumentUpload> DocumentUploads { get; set; }
		public virtual ICollection<Contact> Contacts { get; set; }

        public virtual Residence Residence { get; set; }
	}
}
