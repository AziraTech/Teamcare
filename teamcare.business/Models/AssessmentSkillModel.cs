using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using teamcare.common.Enumerations;

namespace teamcare.business.Models
{
    public class AssessmentSkillModel : BaseModel
    {
        public Guid AssessmentId { get; set; }

        public Guid? SkillId { get; set; }

        public string SkillGroup { get; set; }

        public string SkillName { get; set; }

        public AssessmentSkillLevel SkillLevel { get; set; }
        public RiskAssessmentLevel RiskLevel { get; set; }
        public InitialAssessmentSections InitialAssessmentSection { get; set; }
        public string InitialAssessmentContent { get; set; }
    }
}
