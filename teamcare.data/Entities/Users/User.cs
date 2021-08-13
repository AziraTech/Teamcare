using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using teamcare.common.Enumerations;

namespace teamcare.data.Entities
{
	[Table("Users")]
	public class User : BaseEntity
	{
		[Column("first_name")]
		public string FirstName { get; set; }

		[Column("last_name")]
		public string LastName { get; set; }

		[Column("email")]
		public string Email { get; set; }

		[Column("user_role")]
		[Required]
		public UserRoles UserRole { get; set; }

		[Column("is_active")]
		[Required]
		public bool IsActive { get; set; }
	}
}
