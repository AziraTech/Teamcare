using System.Collections.Generic;
using System.Linq;
using teamcare.common.Enumerations;

namespace teamcare.business.Models
{
	public class UserModel : BaseModel
	{
		public NameTitle Title { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public UserRoles UserRole { get; set; }

		public bool IsActive { get; set; }
		public string Position { get; set; }
		public string Mobile_No { get; set; }
		public string Phone_No { get; set; }
		public string Address { get; set; }
		
		public string PrePath { get; set; }
		public ICollection<DocumentUploadModel> DocumentUploads { get; set; }

		public DocumentUploadModel ProfilePhoto => DocumentUploads?.OrderByDescending(x=>x.CreatedOn).FirstOrDefault(i => i.IsTemporary == false && i.DocumentType == (int)DocumentTypes.ProfilePhoto);
	}
}
