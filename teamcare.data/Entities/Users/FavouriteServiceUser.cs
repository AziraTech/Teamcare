using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace teamcare.data.Entities.Users
{
	[Table("FavouriteServiceUsers")]
	public class FavouriteServiceUser:BaseEntity
	{
		[ForeignKey("User")]
		[Column("user_id")]
		public Guid? UserId { get; set; }
		public virtual User User { get; set; }

		[ForeignKey("ServiceUser")]
		[Column("service_user_id")]
		public Guid ServiceUserId { get; set; }
		public virtual ServiceUser ServiceUser { get; set; }
	}
}
