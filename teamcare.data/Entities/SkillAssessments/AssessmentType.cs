using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using teamcare.common.Enumerations;

namespace teamcare.data.Entities.SkillAssessments
{
	[Table("AssessmentTypes")]
	public class AssessmentType:BaseEntity
	{
		[Column("type_name")]
		[Required]
		public string TypeName { get; set; }

		[Column("options_group")]
		public AssessmentOptionsGroup OptionsGroup { get; set; }

		[Column("position")]
		public int Position { get; set; } = 0;
	}
}
