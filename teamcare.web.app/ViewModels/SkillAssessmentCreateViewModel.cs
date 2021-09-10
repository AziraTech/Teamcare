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
    public class SkillAssessmentCreateViewModel : BaseViewModel
    {
        public SkillAssessmentCreateViewModel()
        {

        }
        public SkillAssessmentCreateViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {

        }
        public SkillGroupsModel SkillGroups { get; set; }
        public LivingSkillsModel LivingSkills { get; set; }
       

    }
}