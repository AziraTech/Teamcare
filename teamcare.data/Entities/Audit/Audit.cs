using System.ComponentModel.DataAnnotations.Schema;

namespace teamcare.data.Entities
{
	[Table("Audits")]
	public class Audit : BaseEntity
	{
		[Column("action")]
		public string Action { get; set; }

		[Column("details")]
		public string Details { get; set; }

		[Column("user_reference")]
		public string UserReference { get; set; }
	}
}
