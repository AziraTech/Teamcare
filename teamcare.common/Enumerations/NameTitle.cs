using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace teamcare.common.Enumerations
{
	public enum NameTitle
	{
		[Description("Mr.")]
		Mr = 1,
		[Description("Miss")]
		Miss = 2,
		[Description("Dr.")]
		Dr = 3,
		[Description("Er.")]
		Er = 4
	}
}
