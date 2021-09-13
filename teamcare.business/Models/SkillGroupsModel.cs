using System;
using System.Collections.Generic;
using System.Text;

namespace teamcare.business.Models
{
    public class SkillGroupsModel : BaseModel
    {
        public string GroupName { get; set; }
        public int Position { get; set; }
        public int TotalSkill { get; set; }

    }
}
