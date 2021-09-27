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
		View = 4,
		[Description("Sign Out")]
		SignOut = 5,
		[Description("Home Page")]
		HomePage = 6,
		[Description("Status Change")]
		StatusChange = 7,
		[Description("Sign In")]
		SignIn = 8,

	}
}
