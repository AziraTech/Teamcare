using System;
using System.Collections.Generic;
using System.Text;
using teamcare.common.Enumerations;

namespace teamcare.business.Models
{
	public class AuditModel : BaseModel
	{
		public AuditAction Action { get; set; }
		public string Details { get; set; }
		public string UserReference { get; set; }
		public string UserName { get; set; }
	}
}
