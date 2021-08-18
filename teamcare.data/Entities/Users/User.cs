using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using teamcare.common.Enumerations;
using teamcare.data.Entities.Documents;

namespace teamcare.data.Entities
{
	[Table("Users")]
	public class User : BaseEntity
	{
		[Column("title")]
		public string Title { get; set; }

		[Column("first_name")]
		public string FirstName { get; set; }

		[Column("last_name")]
		public string LastName { get; set; }

		[Column("email")]
		public string Email { get; set; }

		[Column("user_role")]
		[Required]
		public UserRoles UserRole { get; set; }

		[Column("position")]
		public string Position { get; set; }

		[Column("mobile_no")]
		public string Mobile_No { get; set; }

		[Column("phone_no")]
		public string Phone_No { get; set; }

		[Column("address")]
		public string Address { get; set; }

		[Column("is_active")]
		[Required]
		public bool IsActive { get; set; }
		public virtual ICollection<DocumentUpload> DocumentUploads { get; set; }

	}
}
