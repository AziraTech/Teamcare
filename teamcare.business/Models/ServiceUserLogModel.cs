﻿using System;
using teamcare.data.Entities;

namespace teamcare.business.Models
{
    
	public class ServiceUserLogModel : BaseModel
	{
		public Guid? ActionByAdminId { get; set; }
		public User ActionByAdmin { get; set; }
		public Guid LogCreatedFor { get; set; }
		public ServiceUser ServiceUser { get; set; }				
		public string LogMessage { get; set; }
		public bool IsApproved { get; set; }
		public bool IsVisible { get; set; }
		public string LogMessageUpdated { get; set; }
		public DateTimeOffset? AdminActionOn { get; set; }
	}
}
