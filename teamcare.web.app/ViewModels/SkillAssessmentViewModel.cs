using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using teamcare.common.Configuration;

namespace teamcare.web.app.ViewModels
{
    public class SkillAssessmentViewModel : BaseViewModel
    {
        public SkillAssessmentViewModel()
        {

        }
        public SkillAssessmentViewModel(IOptions<AzureStorageSettings> azureStorageOptions) : base(azureStorageOptions)
        {

        }
        public IEnumerable<business.Models.SkillGroupsModel> SkillGroups { get; set; }
        public IEnumerable<business.Models.LivingSkillsModel> LivingSkills { get; set; }
        public business.Models.SkillGroupsModel SkillGroup { get; set; }
        public business.Models.LivingSkillsModel LivingSkill { get; set; }
        public SkillAssessmentCreateViewModel CreateViewModel { get; set; }
    }
}
