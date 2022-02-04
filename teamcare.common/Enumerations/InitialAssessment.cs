using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace teamcare.common.Enumerations
{
	public enum InitialAssessmentSections
	{
		[Description("Profile")]
		Profile = 1,
		[Description("Behaviour")]
		Behaviour = 2,
		[Description("Cognition")]
		Cognition = 3,
		[Description("Psychological Needs")]
		PsychologicalNeeds = 4,
		[Description("Communication")]
		Communication=5,

		[Description("Mobility")]
		Mobility=6,
		[Description("Nutrition - Food & Drink")]
		NutritionFoodAndDrink=7,
		[Description("Continence")]
		Continence=8,
		[Description("Skin (including tissue viability)")]
		Skin=9,
		[Description("Breathing")]
		Breathing=10,

		[Description("Drug Therapies & Medication")]
		DrugTherapiesAndMedication=11,
		[Description("Altered State of Consciousness")]
		AlteredStateOfConsciousness=12,
		[Description("Independent Skills")]
		IndependentSkills = 11,
		[Description("Summary of Needs")]
		SummaryOfNeeds=12
	}
}
