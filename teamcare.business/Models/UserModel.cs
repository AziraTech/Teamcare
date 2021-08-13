using teamcare.common.Enumerations;

namespace teamcare.business.Models
{
	public class UserModel : BaseModel
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public UserRoles UserRole { get; set; }

		public bool IsActive { get; set; }
	}
}
