using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace teamcare.common.Enumerations
{
	public enum ArchiveReason
	{
		[Description("Deceased Transferred")]
		DeceasedTransferred = 1,
		[Description("Left")]
		Left = 2
	}
}
