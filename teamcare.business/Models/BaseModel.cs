using System;

namespace teamcare.business.Models
{
	public class BaseModel
	{
		public Guid? Id { get; set; }

		public Guid? CreatedBy { get; set; }
		public virtual UserModel CreatedByUser { get; set; }

		public Guid? UpdatedBy { get; set; }
		public virtual UserModel UpdatedByUser { get; set; }

		public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

		public DateTimeOffset? UpdatedOn { get; set; }

		public Guid? DeletedBy { get; set; }
		public virtual UserModel DeletedByUser { get; set; }

		public DateTimeOffset? DeletedOn { get; set; }
	}
}
