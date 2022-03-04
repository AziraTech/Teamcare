using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace teamcare.common.Enumerations
{
	public enum ServiceUserLogCategory
	{
		[Description("Medical/Appointments")]
		MedicalAppointments = 1,
		[Description("Incident/Accident")]
		IncidentAccident = 2,
		[Description("Observation")]
		Observation = 3
	}
}
