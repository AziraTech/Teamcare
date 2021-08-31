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
		[ForeignKey("User")]
		[Column("log_created_by")]
		public Guid LogCreatedBy { get; set; }
		public virtual User User  { get; set; }

		[ForeignKey("ServiceUser")]
		[Column("log_created_for")]
		public Guid LogCreatedFor { get; set; }
		public virtual ServiceUser ServiceUser { get; set; }

		[Column("log_message")]
		public string LogMessage { get; set; }

	}
}
