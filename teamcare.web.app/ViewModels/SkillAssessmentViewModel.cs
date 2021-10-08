using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using teamcare.common.Configuration;
using teamcare.common.Models;

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
        public IEnumerable<business.Models.AssessmentTypeModel>  AssessmentType { get; set; }
        public IEnumerable<EnumListItem> AssessmentSkillLevel { get; set; }
        public Guid AssessmentTypeId { get; set; }
        public Guid ServiceUserId { get; set; }
        public IEnumerable<business.Models.AssessmentSkillModel> AssessmentSkill { get; set; }
        public IEnumerable<business.Models.AssessmentModel> AssessmentList { get; set; }
        public bool IsDueAssessment { get; set; }
        public IEnumerable<EnumListItem> RiskAssessment { get; set; }
        public business.Models.AssessmentSkillModel RiskAssessmentSkill { get; set; }
        public IEnumerable<EnumListItem> AssessmentOptionsGroup { get; set; }


    }
}
