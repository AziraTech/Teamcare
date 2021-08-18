﻿// <auto-generated />
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
    [Migration("20210818080527_AddUsersFields")]
    partial class AddUsersFields
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

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("ResidenceId");

                    b.HasIndex("ServiceUserId");

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

                    b.Property<string>("Ethnicity")
                        .HasColumnType("text")
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

                    b.Property<string>("MaritalStatus")
                        .HasColumnType("text")
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

                    b.Property<string>("PreferredFirstLanguage")
                        .HasColumnType("text")
                        .HasColumnName("preferred_first_language");

                    b.Property<string>("RelationshipToPerson")
                        .HasColumnType("text")
                        .HasColumnName("relationship_to_person");

                    b.Property<string>("Religion")
                        .HasColumnType("text")
                        .HasColumnName("religion");

                    b.Property<Guid>("ResidenceId")
                        .HasColumnType("uuid")
                        .HasColumnName("residence_id");

                    b.Property<string>("Title")
                        .HasColumnType("text")
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
                            Id = new Guid("63245849-c8ea-4484-b954-82a1e688a0e8"),
                            CreatedOn = new DateTimeOffset(new DateTime(2021, 8, 18, 8, 5, 25, 862, DateTimeKind.Unspecified).AddTicks(3447), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "hans.doyekee@gmail.com",
                            FirstName = "Teamcare",
                            IsActive = false,
                            LastName = "Admin",
                            UserRole = 1
                        },
                        new
                        {
                            Id = new Guid("23312baf-9613-4c89-a84b-abce8cf454a6"),
                            CreatedOn = new DateTimeOffset(new DateTime(2021, 8, 18, 8, 5, 25, 862, DateTimeKind.Unspecified).AddTicks(8924), new TimeSpan(0, 0, 0, 0, 0)),
                            Email = "nish.kagathara0791@gmail.com",
                            FirstName = "Nishidh",
                            IsActive = false,
                            LastName = "Kagathara",
                            UserRole = 1
                        });
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
                    b.HasOne("teamcare.data.Entities.User", "CreatedByUser")
                        .WithMany("DocumentUploads")
                        .HasForeignKey("CreatedBy");

                    b.HasOne("teamcare.data.Entities.Residence", "Residence")
                        .WithMany("DocumentUploads")
                        .HasForeignKey("ResidenceId");

                    b.HasOne("teamcare.data.Entities.ServiceUser", "ServiceUser")
                        .WithMany("DocumentUploads")
                        .HasForeignKey("ServiceUserId");

                    b.Navigation("CreatedByUser");

                    b.Navigation("Residence");

                    b.Navigation("ServiceUser");
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

            modelBuilder.Entity("teamcare.data.Entities.User", b =>
                {
                    b.HasOne("teamcare.data.Entities.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("teamcare.data.Entities.Residence", b =>
                {
                    b.Navigation("DocumentUploads");

                    b.Navigation("ServiceUsers");
                });

            modelBuilder.Entity("teamcare.data.Entities.ServiceUser", b =>
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
