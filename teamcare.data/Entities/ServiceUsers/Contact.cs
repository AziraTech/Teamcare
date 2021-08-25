using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using teamcare.common.Enumerations;
using teamcare.data.Entities.Documents;

namespace teamcare.data.Entities.ServiceUsers
{
	[Table("Contacts")]
	public class Contact:BaseEntity
	{
		[ForeignKey("ServiceUser")]
		[Column("service_user_id")]
		public Guid ServiceUserId { get; set; }
		public virtual ServiceUser ServiceUser { get; set; }

		[Column("title")]
		public NameTitle Title { get; set; }

		[Column("first_name")]
		public string FirstName { get; set; }

		[Column("middle_name")]
		public string MiddleName { get; set; }

		[Column("last_name")]
		public string LastName { get; set; }

		[Column("address")]
		public string Address { get; set; }

		[Column("email")]
		public string Email { get; set; }

		[Column("mobile")]
		public string Mobile { get; set; }

		[Column("telephone")]
		public string Telephone { get; set; }

		[Column("relationship")]
		public Relationship Relationship { get; set; }

		[Column("is_next_of_kin")]
		public bool IsNextOfKin { get; set; }

		[Column("is_emergency_contact")]
		public bool IsEmergencyContact { get; set; }

		public virtual ICollection<DocumentUpload> DocumentUploads { get; set; }
	}
}
