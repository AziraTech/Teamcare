using Microsoft.EntityFrameworkCore;
using teamcare.data.Entities;
using teamcare.data.Entities.Documents;
using teamcare.data.Entities.ServiceUsers;
using teamcare.data.Entities.SkillAssessments;
using teamcare.data.Entities.Users;

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
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<FavouriteServiceUser> FavouriteServiceUsers { get; set; }
        public DbSet<ServiceUserLog> ServiceUserLogs { get; set; }
        public DbSet<SkillGroup> SkillGroups { get; set; }
        public DbSet<LivingSkill> LivingSkills { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<AssessmentSkill> AssessmentSkills { get; set; }
        public DbSet<AssessmentType> AssessmentTypes { get; set; }
        public DbSet<HealthMedication> HealthMedications { get; set; }
        public DbSet<ServiceUserDocument> ServiceUserDocuments { get; set; }
        public DbSet<BloodPressureReading> BloodPressureReadings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentUpload>().HasOne(i => i.User).WithMany(u => u.DocumentUploads)
                .HasForeignKey(i => i.UserId);

            base.OnModelCreating(modelBuilder);
            MasterDataSeed.SeedMasterData(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(true);
            base.OnConfiguring(optionsBuilder);
        }
    }
}

