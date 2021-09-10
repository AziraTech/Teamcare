using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace teamcare.data.Entities.SkillAssessments
{
	[Table("LivingSkills")]
	public class LivingSkill:BaseEntity
	{
		[ForeignKey("SkillGroup")]
		[Column("group_id")]
		public Guid GroupId { get; set; }
		public virtual SkillGroup SkillGroup { get; set; }

		[Column("skill_name")]
		[Required]
		public string SkillName { get; set; }

		[Column("skill_position")]
		public int Position { get; set; } = 0;
	}
}
