using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace teamcare.common.Enumerations
{
	public enum DocumentCategories
	{
		[Description("Correspondence")]
		Correspondence = 1,
		[Description("Medical Letter")]
		MedicalLetter = 2,
		[Description("Council Document")]
		CouncilDocument = 3,
		[Description("Other")]
		Other = 4
	}
}