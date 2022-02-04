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

	public enum ArchiveReasonResidence
	{
		[Description("Sold")]
		Sold = 1,
		[Description("Not in use")]
		NotInUse = 2
	}
}
