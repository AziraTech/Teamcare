using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace teamcare.common.Enumerations
{
	public enum Relationship
	{
		[Description("Sibling")]
		Sibling = 1,
		[Description("Spouse")]
		Spouse = 2,
		[Description("Parent")]
		Parent = 3,
		[Description("Child")]
		Child = 4,
		[Description("Grand Parent")]
		GP = 5,
		[Description("Other")]
		Other = 6,
	}
}
