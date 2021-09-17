using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using teamcare.common.Enumerations;

namespace teamcare.data.Entities.SkillAssessments
{
	[Table("SkillGroups")]
	public class SkillGroup : BaseEntity
	{
		[Column("group_name")]
		[Required]
		public string GroupName { get; set; }

		[Column("group_position")]
		public int Position { get; set; } = 0;

		[Column("assessment_type")]
		public AssessmentType AssessmentType { get; set; }
	}
}
