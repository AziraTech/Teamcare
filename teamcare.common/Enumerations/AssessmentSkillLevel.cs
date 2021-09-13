using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace teamcare.common.Enumerations
{
    public enum AssessmentSkillLevel
    {
        [Description("Independent (I)")]
        Independent = 1,
        [Description("Partly Independent (PI)")]
        PartlyIndependent = 2,
        [Description("Verbal Prompt (P)")]
        VerbalPrompt = 3,
        [Description("Distant Supervision (S)")]
        DistantSupervision = 4,
        [Description("Fully Supported (FS)")]
        FullySupported = 5,
        [Description("Not Applicable/Required (NA)")]
        NotApplicableRequired = 6,

    }
}
