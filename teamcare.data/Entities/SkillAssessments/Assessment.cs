using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using teamcare.common.Enumerations;

namespace teamcare.data.Entities.SkillAssessments
{
	[Table("Assessments")]
	public class Assessment : BaseEntity
	{
		[ForeignKey("ServiceUser")]
		[Column("service_user_id")]
		public Guid ServiceUserId { get; set; }
		public virtual ServiceUser ServiceUser { get; set; }

		[Column("assessment_type")]
		public AssessmentType AssessmentType { get; set; }
	}
}
