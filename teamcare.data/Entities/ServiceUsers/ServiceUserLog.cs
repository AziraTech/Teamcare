using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using teamcare.common.Enumerations;
using teamcare.data.Entities;
using teamcare.data.Entities.Documents;
using teamcare.data.Entities.ServiceUsers;
 
namespace teamcare.data.Entities.ServiceUsers
{
	[Table("ServiceUserLog")]
	public class ServiceUserLog : BaseEntity
	{
		[ForeignKey("ServiceUser")]
		[Column("log_created_for")]
		public Guid LogCreatedFor { get; set; }
		public virtual ServiceUser ServiceUser { get; set; }

		[Column("log_message")]
		public string LogMessage { get; set; }

		[Column("log_message_updated")]
		public string LogMessageUpdated { get; set; }

		[Column("is_approved")]
		public bool IsApproved { get; set; } = false;

		[Column("is_visible")]
		public bool IsVisible { get; set; } = true;

		[ForeignKey("ActionByAdmin")]
		[Column("action_by_admin_id")]
		public Guid? ActionByAdminId { get; set; }
		public virtual User? ActionByAdmin { get; set; }

		[Column("admin_action_on")]
		public DateTimeOffset? AdminActionOn { get; set; }

	}
}
