using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using teamcare.common.Enumerations;

namespace teamcare.data.Entities.SkillAssessments
{
	[Table("AssessmentSkills")]
	public class AssessmentSkill:BaseEntity
	{
		[ForeignKey("Assessment")]
		[Column("assessment_id")]
		public Guid AssessmentId { get; set; }
		public virtual Assessment Assessment { get; set; }

		[ForeignKey("LivingSkill")]
		[Column("skill_id")]
		public Guid? SkillId { get; set; }
		public virtual LivingSkill LivingSkill { get; set; }

		[Column("skill_group")]
		public string SkillGroup { get; set; }

		[Column("skill_name")]
		public string SkillName { get; set; }

		[Column("skill_level")]
		public AssessmentSkillLevel SkillLevel { get; set; }
	}
}
