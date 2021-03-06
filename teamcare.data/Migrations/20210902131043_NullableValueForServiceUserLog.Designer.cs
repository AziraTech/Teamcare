// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using teamcare.data.Data;

namespace teamcare.data.Migrations
{
    [DbContext(typeof(TeamcareDbContext))]
    [Migration("20210902131043_NullableValueForServiceUserLog")]
    partial class NullableValueForServiceUserLog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("teamcare.data.Entities.Audit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Action")
                        .HasColumnType("text")
                        .HasColumnName("action");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_by");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_on");

                    b.Property<string>("Details")
                        .HasColumnType("text")
                        .HasColumnName("details");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("last_updated_by");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated_on");

                    b.Property<string>("UserReference")
                        .HasColumnType("text")
                        .HasColumnName("user_reference");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("teamcare.data.Entities.Documents.DocumentUpload", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("BlobName")
                        .HasColumnType("text")
                        .HasColumnName("blob_name");

                    b.Property<Guid?>("ContactId")
                        .HasColumnType("uuid")
                        .HasColumnName("contact_id");

                    b.Property<string>("ContentType")
                        .HasColumnType("text")
                        .HasColumnName("content_type");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_by");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_on");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("DocumentSubtype")
                        .HasColumnType("text")
                        .HasColumnName("document_subtype");

                    b.Property<int>("DocumentType")
                        .HasColumnType("integer")
                        .HasColumnName("document_type");

                    b.Property<string>("FileExtension")
                        .HasColumnType("text")
                        .HasColumnName("file_extension");

                    b.Property<string>("FileName")
                        .HasColumnType("text")
                        .HasColumnName("file_name");

                    b.Property<long>("FileSizeInBytes")
                        .HasColumnType("bigint")
                        .HasColumnName("file_size_in_bytes");

                    b.Property<bool>("IsTemporary")
                        .HasColumnType("boolean")
                        .HasColumnName("is_temporary");

                    b.Property<Guid?>("ResidenceId")
                        .HasColumnType("uuid")
                        .HasColumnName("residence_id");

                    b.Property<Guid?>("ServiceUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("service_user_id");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("last_updated_by");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated_on");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ResidenceId");

                    b.HasIndex("ServiceUserId");

                    b.HasIndex("UserId");

                    b.ToTable("DocumentUploads");
                });

            modelBuilder.Entity("teamcare.data.Entities.MedicalHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_by");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_on");

                    b.Property<string>("Diagnosis")
                        .HasColumnType("text")
                        .HasColumnName("diagnosis");

                    b.Property<string>("GPAddress")
                        .HasColumnType("text")
                        .HasColumnName("gp_address");

                    b.Property<string>("GPEmailId")
                        .HasColumnType("text")
                        .HasColumnName("gp_email_id");

                    b.Property<long>("GPFaxNo")
                        .HasColumnType("bigint")
                        .HasColumnName("gp_fax_no");

                    b.Property<string>("GPName")
                        .HasColumnType("text")
                        .HasColumnName("gp_name");

                    b.Property<long>("GPTelNo")
                        .HasColumnType("bigint")
                        .HasColumnName("gp_tel_no");

                    b.Property<string>("OtherSignificantInformation")
                        .HasColumnType("text")
                        .HasColumnName("other_significant_information");

                    b.Property<Guid>("ServiceUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("service_user_id");

                    b.Property<string>("SychiatristAddress")
                        .HasColumnType("text")
                        .HasColumnName("sychiatrist_address");

                    b.Property<string>("SychiatristEmailId")
                        .HasColumnType("text")
                        .HasColumnName("sychiatrist_email_id");

                    b.Property<long>("SychiatristFaxNo")
                        .HasColumnType("bigint")
                        .HasColumnName("sychiatrist_fax_no");

                    b.Property<string>("SychiatristName")
                        .HasColumnType("text")
                        .HasColumnName("sychiatrist_name");

                    b.Property<long>("SychiatristTelNo")
                        .HasColumnType("bigint")
                        .HasColumnName("sychiatrist_tel_no");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("last_updated_by");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated_on");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ServiceUserId");

                    b.ToTable("MedicalHistories");
                });

            modelBuilder.Entity("teamcare.data.Entities.Residence", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<string>("Capacity")
                        .HasColumnType("text")
                        .HasColumnName("capacity");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_by");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_on");

                    b.Property<string>("Home_Tel_No")
                        .HasColumnType("text")
                        .HasColumnName("home_tel_no");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("last_updated_by");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated_on");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Residences");
                });

            modelBuilder.Entity("teamcare.data.Entities.ServiceUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<DateTime?>("ArchivedOn")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("archived_on");

                    b.Property<string>("ArchivedReason")
                        .HasColumnType("text")
                        .HasColumnName("archived_reason");

                    b.Property<string>("ContactDetails")
                        .HasColumnType("text")
                        .HasColumnName("contact_details");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<string>("CurrentPreviousOccupation")
                        .HasColumnType("text")
                        .HasColumnName("current_previous_occupation");

                    b.Property<DateTime>("DateOfAdmission")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_of_admission");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_by");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_on");

                    b.Property<int>("Ethnicity")
                        .HasColumnType("integer")
                        .HasColumnName("ethnicity");

                    b.Property<string>("FirstName")
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("KnownAs")
                        .HasColumnType("text")
                        .HasColumnName("known_as");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("LegalStatus")
                        .HasColumnType("text")
                        .HasColumnName("legal_status");

                    b.Property<int>("MaritalStatus")
                        .HasColumnType("integer")
                        .HasColumnName("marital_status");

                    b.Property<string>("NHSIdNumber")
                        .HasColumnType("text")
                        .HasColumnName("nhs_id_number");

                    b.Property<string>("NationalInsuranceNo")
                        .HasColumnType("text")
                        .HasColumnName("national_insurance_no");

                    b.Property<string>("NextOfKin")
                        .HasColumnType("text")
                        .HasColumnName("next_of_kin");

                    b.Property<string>("PersonalTelNo")
                        .HasColumnType("text")
                        .HasColumnName("personal_tel_no");

                    b.Property<int>("PreferredFirstLanguage")
                        .HasColumnType("integer")
                        .HasColumnName("preferred_first_language");

                    b.Property<string>("RelationshipToPerson")
                        .HasColumnType("text")
                        .HasColumnName("relationship_to_person");

                    b.Property<int>("Religion")
                        .HasColumnType("integer")
                        .HasColumnName("religion");

                    b.Property<Guid>("ResidenceId")
                        .HasColumnType("uuid")
                        .HasColumnName("residence_id");

                    b.Property<int>("Title")
                        .HasColumnType("integer")
                        .HasColumnName("title");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("last_updated_by");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated_on");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ResidenceId");

                    b.ToTable("ServiceUsers");
                });

            modelBuilder.Entity("teamcare.data.Entities.ServiceUsers.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_by");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_on");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsEmergencyContact")
                        .HasColumnType("boolean")
                        .HasColumnName("is_emergency_contact");

                    b.Property<bool>("IsNextOfKin")
                        .HasColumnType("boolean")
                        .HasColumnName("is_next_of_kin");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text")
                        .HasColumnName("middle_name");

                    b.Property<string>("Mobile")
                        .HasColumnType("text")
                        .HasColumnName("mobile");

                    b.Property<int>("Relationship")
                        .HasColumnType("integer")
                        .HasColumnName("relationship");

                    b.Property<Guid>("ServiceUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("service_user_id");

                    b.Property<string>("Telephone")
                        .HasColumnType("text")
                        .HasColumnName("telephone");

                    b.Property<int>("Title")
                        .HasColumnType("integer")
                        .HasColumnName("title");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("last_updated_by");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated_on");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ServiceUserId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("teamcare.data.Entities.ServiceUsers.ServiceUserLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("ActionByAdminId")
                        .HasColumnType("uuid")
                        .HasColumnName("action_by_admin_id");

                    b.Property<DateTimeOffset?>("AdminActionOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("admin_action_on");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_by");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_on");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean")
                        .HasColumnName("is_approved");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("boolean")
                        .HasColumnName("is_visible");

                    b.Property<Guid>("LogCreatedFor")
                        .HasColumnType("uuid")
                        .HasColumnName("log_created_for");

                    b.Property<string>("LogMessage")
                        .HasColumnType("text")
                        .HasColumnName("log_message");

                    b.Property<string>("LogMessageUpdated")
                        .HasColumnType("text")
                        .HasColumnName("log_message_updated");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("last_updated_by");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated_on");

                    b.HasKey("Id");

                    b.HasIndex("ActionByAdminId");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("LogCreatedFor");

                    b.ToTable("ServiceUserLog");
                });

            modelBuilder.Entity("teamcare.data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_by");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_on");

                    b.Property<string>("Email")
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean")
                        .HasColumnName("is_active");

                    b.Property<string>("LastName")
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("Mobile_No")
                        .HasColumnType("text")
                        .HasColumnName("mobile_no");

                    b.Property<string>("Phone_No")
                        .HasColumnType("text")
                        .HasColumnName("phone_no");

                    b.Property<string>("Position")
                        .HasColumnType("text")
                        .HasColumnName("position");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("last_updated_by");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated_on");

                    b.Property<int>("UserRole")
                        .HasColumnType("integer")
                        .HasColumnName("user_role");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3e32a1c5-3de2-4335-a441-66c3eca9dc74"),
                            CreatedOn = new DateTimeOffset(new DateTime(2021, 9, 2, 13, 10, 42, 749, DateTimeKind.Unspecified).AddTicks(5049), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "hans.doyekee@gmail.com",
                            FirstName = "Teamcare",
                            IsActive = true,
                            LastName = "Admin",
                            UserRole = 1
                        },
                        new
                        {
                            Id = new Guid("58773bd5-9b3e-45b9-8276-c421b5230c8b"),
                            CreatedOn = new DateTimeOffset(new DateTime(2021, 9, 2, 13, 10, 42, 749, DateTimeKind.Unspecified).AddTicks(6122), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "nish.kagathara0791@gmail.com",
                            FirstName = "Nishidh",
                            IsActive = true,
                            LastName = "Kagathara",
                            UserRole = 1
                        });
                });

            modelBuilder.Entity("teamcare.data.Entities.Users.FavouriteServiceUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid?>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_on");

                    b.Property<Guid?>("DeletedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_by");

                    b.Property<DateTimeOffset?>("DeletedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_on");

                    b.Property<Guid>("ServiceUserId")
                        .HasColumnType("uuid")
                        .HasColumnName("service_user_id");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("last_updated_by");

                    b.Property<DateTimeOffset?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated_on");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ServiceUserId");

                    b.HasIndex("UserId");

                    b.ToTable("FavouriteServiceUsers");
                });

            modelBuilder.Entity("teamcare.data.Entities.Audit", b =>
                {
                    b.HasOne("teamcare.data.Entities.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("teamcare.data.Entities.Documents.DocumentUpload", b =>
                {
                    b.HasOne("teamcare.data.Entities.ServiceUsers.Contact", "Contact")
                        .WithMany("DocumentUploads")
                        .HasForeignKey("ContactId");

                    b.HasOne("teamcare.data.Entities.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("teamcare.data.Entities.Residence", "Residence")
                        .WithMany("DocumentUploads")
                        .HasForeignKey("ResidenceId");

                    b.HasOne("teamcare.data.Entities.ServiceUser", "ServiceUser")
                        .WithMany("DocumentUploads")
                        .HasForeignKey("ServiceUserId");

                    b.HasOne("teamcare.data.Entities.User", "User")
                        .WithMany("DocumentUploads")
                        .HasForeignKey("UserId");

                    b.Navigation("Contact");

                    b.Navigation("CreatedByUser");

                    b.Navigation("Residence");

                    b.Navigation("ServiceUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("teamcare.data.Entities.MedicalHistory", b =>
                {
                    b.HasOne("teamcare.data.Entities.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("teamcare.data.Entities.ServiceUser", "ServiceUser")
                        .WithMany()
                        .HasForeignKey("ServiceUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByUser");

                    b.Navigation("ServiceUser");
                });

            modelBuilder.Entity("teamcare.data.Entities.Residence", b =>
                {
                    b.HasOne("teamcare.data.Entities.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("teamcare.data.Entities.ServiceUser", b =>
                {
                    b.HasOne("teamcare.data.Entities.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("teamcare.data.Entities.Residence", "Residence")
                        .WithMany("ServiceUsers")
                        .HasForeignKey("ResidenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByUser");

                    b.Navigation("Residence");
                });

            modelBuilder.Entity("teamcare.data.Entities.ServiceUsers.Contact", b =>
                {
                    b.HasOne("teamcare.data.Entities.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("teamcare.data.Entities.ServiceUser", "ServiceUser")
                        .WithMany("Contacts")
                        .HasForeignKey("ServiceUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByUser");

                    b.Navigation("ServiceUser");
                });

            modelBuilder.Entity("teamcare.data.Entities.ServiceUsers.ServiceUserLog", b =>
                {
                    b.HasOne("teamcare.data.Entities.User", "ActionByAdmin")
                        .WithMany()
                        .HasForeignKey("ActionByAdminId");

                    b.HasOne("teamcare.data.Entities.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("teamcare.data.Entities.ServiceUser", "ServiceUser")
                        .WithMany("ServiceUserLog")
                        .HasForeignKey("LogCreatedFor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActionByAdmin");

                    b.Navigation("CreatedByUser");

                    b.Navigation("ServiceUser");
                });

            modelBuilder.Entity("teamcare.data.Entities.User", b =>
                {
                    b.HasOne("teamcare.data.Entities.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("teamcare.data.Entities.Users.FavouriteServiceUser", b =>
                {
                    b.HasOne("teamcare.data.Entities.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("teamcare.data.Entities.ServiceUser", "ServiceUser")
                        .WithMany()
                        .HasForeignKey("ServiceUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("teamcare.data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("CreatedByUser");

                    b.Navigation("ServiceUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("teamcare.data.Entities.Residence", b =>
                {
                    b.Navigation("DocumentUploads");

                    b.Navigation("ServiceUsers");
                });

            modelBuilder.Entity("teamcare.data.Entities.ServiceUser", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("DocumentUploads");

                    b.Navigation("ServiceUserLog");
                });

            modelBuilder.Entity("teamcare.data.Entities.ServiceUsers.Contact", b =>
                {
                    b.Navigation("DocumentUploads");
                });

            modelBuilder.Entity("teamcare.data.Entities.User", b =>
                {
                    b.Navigation("DocumentUploads");
                });
#pragma warning restore 612, 618
        }
    }
}
