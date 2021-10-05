using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace teamcare.common.Enumerations
{
    public enum RiskAssessmentLevel
    {
        [Description("High")]
        High = 1,
        [Description("Medium")]
        Medium = 2,
        [Description("Low")]
        Low = 3,
        [Description("Negligible")]
        Negligible = 4,
      
    }
}
