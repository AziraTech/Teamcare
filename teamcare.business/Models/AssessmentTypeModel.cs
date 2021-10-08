using System;
using System.Collections.Generic;
using System.Text;
using teamcare.common.Enumerations;

namespace teamcare.business.Models
{
	public class AssessmentTypeModel : BaseModel
	{	
		public string TypeName { get; set; }
		public AssessmentOptionsGroup OptionsGroup { get; set; }
		public int Position { get; set; }


	}
}
