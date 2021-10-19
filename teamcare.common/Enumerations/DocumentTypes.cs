using System.ComponentModel;

namespace teamcare.common.Enumerations
{
    public enum DocumentTypes
    {
		[Description("Profile Photo")]
		ProfilePhoto = 1,
		[Description("Correspondence")]
		Correspondence = 2,
		[Description("Medical Letter")]
		MedicalLetter = 3,
		[Description("Council Document")]
		CouncilDocument = 4,
		[Description("Other")]
		Other = 5
	}
}

