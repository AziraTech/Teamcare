using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using teamcare.business.Models;
using teamcare.common.Configuration;
using teamcare.common.Models;

namespace teamcare.web.app.ViewModels
{
    public class AssessmentCreateViewModel : BaseViewModel
    {
        public AssessmentCreateViewModel()
        {

        }
     
        public AssessmentModel Assessment { get; set; }
        public AssessmentSkillModel assessmentSkill { get; set; }     

    }
}