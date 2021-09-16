using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using teamcare.common.Configuration;
using teamcare.common.Models;

namespace teamcare.web.app.ViewModels
{
    public class AssessmentViewModel : BaseViewModel
    {
        public AssessmentViewModel()
        {

        }
    
        public IEnumerable<business.Models.AssessmentModel> Assessments { get; set; }
        public IEnumerable<business.Models.AssessmentSkillModel> AssessmentSkills { get; set; }
        public business.Models.AssessmentModel Assessment { get; set; }
        public business.Models.AssessmentSkillModel SkillModel { get; set; }
        public AssessmentCreateViewModel CreateViewModel { get; set; }
      
    }
}
