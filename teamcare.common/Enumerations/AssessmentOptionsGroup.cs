using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace teamcare.common.Enumerations
{
	public enum AssessmentOptionsGroup
	{
		[Description("Living Skill")]
		LivingSkill = 1,
		[Description("Risk")]
		Risk = 2,
		[Description("Initial Assessment")]
		InitialAssessment = 3
	}
}
