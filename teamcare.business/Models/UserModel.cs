using System.Collections.Generic;
using System.Linq;
using teamcare.common.Enumerations;

namespace teamcare.business.Models
{
    public class UserModel : BaseModel
    {
        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int UserRole { get; set; }
        public string Position { get; set; }
        public string Mobile_No { get; set; }
        public string Phone_No { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        public ICollection<DocumentUploadModel> DocumentUploads { get; set; }

        public DocumentUploadModel ProfilePhoto => DocumentUploads?.FirstOrDefault(i => i.IsTemporary == false && i.DocumentType == (int)DocumentTypes.ProfilePhoto);


    }
}
