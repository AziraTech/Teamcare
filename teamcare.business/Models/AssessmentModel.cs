using System;
using System.Collections.Generic;
using System.Text;
using teamcare.common.Enumerations;

namespace teamcare.business.Models
{
	public class AssessmentModel : BaseModel
	{	
		public Guid ServiceUserId { get; set; }
		public AssessmentType AssessmentType { get; set; }
		public string UserName { get; set; }
	}
}
