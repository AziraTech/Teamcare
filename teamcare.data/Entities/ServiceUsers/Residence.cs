using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using teamcare.data.Entities;
using teamcare.data.Entities.Documents;

namespace teamcare.data.Entities
{
	[Table("Residences")]
	public class Residence : BaseEntity
	{
        [Column("name")]
		public string Name { get; set; }

		[Column("address")]
		public string Address { get; set; }

		[Column("capacity")]
		public string Capacity { get; set; }

		[Column("home_tel_no")]
		public string Home_Tel_No { get; set; }

		public virtual ICollection<DocumentUpload> DocumentUploads { get; set; }
		public virtual ICollection<ServiceUser> ServiceUsers { get; set; }

	}
}