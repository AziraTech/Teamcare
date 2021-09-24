using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace teamcare.common.Enumerations
{
	public enum AuditAction
	{
		[Description("Create")]
		Create = 1,
		[Description("Update")]
		Update = 2,
		[Description("Delete")]
		Delete = 3,
		[Description("View")]
		View = 4
	}
}
