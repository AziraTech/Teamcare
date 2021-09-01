using System;
using teamcare.data.Entities;

namespace teamcare.business.Models
{
    
	public class ServiceUserLogModel : BaseModel
	{
		public Guid LogCreatedBy { get; set; }
		public User User { get; set; }
		public Guid LogCreatedFor { get; set; }
		public ServiceUser ServiceUser { get; set; }				
		public string LogMessage { get; set; }
	}
}
