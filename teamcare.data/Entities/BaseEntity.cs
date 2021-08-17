using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace teamcare.data.Entities
{
	public class BaseEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Column("id")]
		public Guid Id { get; set; } = Guid.NewGuid();
		
		
		[Column("created_by")]
		public Guid? CreatedBy { get; set; }
        
        [ForeignKey("CreatedBy")]
		public virtual User CreatedByUser { get; set; }

		[Column("last_updated_by")]
		public Guid? UpdatedBy { get; set; }

		[Column("created_on")]
		public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

		[Column("last_updated_on")]
		public DateTimeOffset? UpdatedOn { get; set; }

		[Column("deleted_by")]
		public Guid? DeletedBy { get; set; }

		[Column("deleted_on")]
		public DateTimeOffset? DeletedOn { get; set; }
	}
}
