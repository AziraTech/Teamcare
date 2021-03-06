using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

		[ForeignKey("AssessmentType")]
		[Column("assessment_type_id")]
		public Guid AssessmentTypeId { get; set; }
		public virtual AssessmentType AssessmentType { get; set; }
	}
}
