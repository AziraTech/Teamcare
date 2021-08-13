using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace teamcare.data.Entities
{
	[Table("MedicalHistories")]
	public class MedicalHistory : BaseEntity
	{
		[ForeignKey("ServiceUser")]
		[Column("service_user_id")]
		public Guid ServiceUserId { get; set; }
		public virtual ServiceUser ServiceUser { get; set; }

		[Column("gp_name")]
		public string GPName { get; set; }

		[Column("gp_address")]
		public string GPAddress { get; set; }

		[Column("gp_tel_no")]
		public Int64 GPTelNo { get; set; }

		[Column("gp_fax_no")]
		public Int64 GPFaxNo { get; set; }

		[Column("gp_email_id")]
		public string GPEmailId { get; set; }

		[Column("sychiatrist_name")]
		public string SychiatristName { get; set; }

		[Column("sychiatrist_address")]
		public string SychiatristAddress { get; set; }

		[Column("sychiatrist_tel_no")]
		public Int64 SychiatristTelNo { get; set; }

		[Column("sychiatrist_fax_no")]
		public Int64 SychiatristFaxNo { get; set; }

		[Column("sychiatrist_email_id")]
		public string SychiatristEmailId { get; set; }

		[Column("other_significant_information")]
		public string OtherSignificantInformation { get; set; }

		[Column("diagnosis")]
		public string Diagnosis { get; set; }

	}
}

