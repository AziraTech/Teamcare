using System;
using System.Collections.Generic;
using System.Text;
using teamcare.data.Entities;

namespace teamcare.business.Models
{
    public class FavouriteServiceUserModel : BaseModel
    {
		public Guid? UserId { get; set; }
		public virtual User User { get; set; }

		public Guid ServiceUserId { get; set; }
		public virtual ServiceUser ServiceUser { get; set; }
	}
}
