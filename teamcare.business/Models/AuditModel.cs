using System;
using System.Collections.Generic;
using System.Text;

namespace teamcare.business.Models
{
	public class AuditModel : BaseModel
	{
		public string Action { get; set; }
		public string Details { get; set; }
		public string UserReference { get; set; }
	}
}
