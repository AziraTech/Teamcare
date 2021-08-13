using Microsoft.EntityFrameworkCore;
using teamcare.data.Entities;
using teamcare.data.Entities.Documents;

namespace teamcare.data.Data
{
	public class TeamcareDbContext : DbContext
	{
		public TeamcareDbContext(DbContextOptions<TeamcareDbContext> options) : base(options)
		{

		}
		public DbSet<User> Users { get; set; }
		public DbSet<Audit> Audit { get; set; }
		public DbSet<ServiceUser> ServiceUsers { get; set; }
		public DbSet<MedicalHistory> MedicalHistories { get; set; }
		public DbSet<Residence> Residences { get; set; }
		public DbSet<DocumentUpload> DocumentUploads { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			MasterDataSeed.SeedMasterData(modelBuilder);
		}
	}
}

