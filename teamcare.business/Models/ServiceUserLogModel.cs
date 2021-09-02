using System;
using System.Collections.Generic;
using System.Linq;
using teamcare.common.Enumerations;
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
		public ICollection<DocumentUploadModel> DocumentUploads { get; set; }
		public DocumentUploadModel ProfilePhoto => DocumentUploads?.FirstOrDefault(i => i.IsTemporary == false && i.DocumentType == (int)DocumentTypes.ProfilePhoto);
		public string PrePath { get; set; }
	}
}
