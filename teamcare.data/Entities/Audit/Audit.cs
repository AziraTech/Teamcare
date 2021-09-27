using System.ComponentModel.DataAnnotations.Schema;
using teamcare.common.Enumerations;

namespace teamcare.data.Entities
{
	[Table("Audits")]
	public class Audit : BaseEntity
	{
		[Column("action")]
		public AuditAction Action { get; set; }

		[Column("details")]
		public string Details { get; set; }

		[Column("user_reference")]
		public string UserReference { get; set; }
	}
}
