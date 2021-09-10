using System;
using System.Collections.Generic;
using System.Text;

namespace teamcare.business.Models
{
    public class LivingSkillsModel : BaseModel
    {
        public Guid GroupId { get; set; }
        public string SkillName { get; set; }
        public int Position { get; set; }

    }
}
