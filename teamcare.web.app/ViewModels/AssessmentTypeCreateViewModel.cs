using System.Collections.Generic;
using Microsoft.Extensions.Options;
using teamcare.business.Models;
using teamcare.common.Configuration;
using teamcare.common.Models;

namespace teamcare.web.app.ViewModels
{
    public class AssessmentTypeCreateViewModel : BaseViewModel
    {
        public AssessmentTypeCreateViewModel()
        {
            
        }
     
        public AssessmentTypeModel AssessmentType { get; set; }
        public IEnumerable<business.Models.AssessmentTypeModel> AssessmentTypeList { get; set; }
        public IEnumerable<EnumListItem> OptionsGroup { get; set; }

    }
}
