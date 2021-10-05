using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace teamcare.common.Enumerations
{
	public enum AssessmentType
	{
		[Description("Living Skills")]
		LivingSkill = 1,
		[Description("Mental Health")]
		MentalHealth = 2,
		[Description("Risk")]
		Risk = 3,
	}
}
