using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    user_role = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    reference = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    last_updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Users_created_by",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    action = table.Column<string>(type: "text", nullable: true),
                    details = table.Column<string>(type: "text", nullable: true),
                    user_reference = table.Column<string>(type: "text", nullable: true),
                    reference = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    last_updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.id);
                    table.ForeignKey(
                        name: "FK_Audits_Users_created_by",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Residences",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    capacity = table.Column<string>(type: "text", nullable: true),
                    home_tel_no = table.Column<string>(type: "text", nullable: true),
                    reference = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    last_updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residences", x => x.id);
                    table.ForeignKey(
                        name: "FK_Residences_Users_created_by",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceUsers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    first_name = table.Column<string>(type: "text", nullable: true),
                    last_name = table.Column<string>(type: "text", nullable: true),
                    known_as = table.Column<string>(type: "text", nullable: true),
                    user_image = table.Column<byte[]>(type: "bytea", nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    legal_status = table.Column<string>(type: "text", nullable: true),
                    nhs_id_number = table.Column<string>(type: "text", nullable: true),
                    national_insurance_no = table.Column<string>(type: "text", nullable: true),
                    date_of_admission = table.Column<string>(type: "text", nullable: true),
                    residence_id = table.Column<Guid>(type: "uuid", nullable: false),
                    personal_tel_no = table.Column<string>(type: "text", nullable: true),
                    marital_status = table.Column<string>(type: "text", nullable: true),
                    religion = table.Column<string>(type: "text", nullable: true),
                    ethnicity = table.Column<string>(type: "text", nullable: true),
                    prefered_first_language = table.Column<string>(type: "text", nullable: true),
                    current_previous_occupation = table.Column<string>(type: "text", nullable: true),
                    next_of_kin = table.Column<string>(type: "text", nullable: true),
                    relationship_to_person = table.Column<string>(type: "text", nullable: true),
                    address = table.Column<string>(type: "text", nullable: true),
                    contact_details = table.Column<string>(type: "text", nullable: true),
                    reference = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    last_updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceUsers", x => x.id);
                    table.ForeignKey(
                        name: "FK_ServiceUsers_Residences_residence_id",
                        column: x => x.residence_id,
                        principalTable: "Residences",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceUsers_Users_created_by",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    gp_name = table.Column<string>(type: "text", nullable: true),
                    gp_address = table.Column<string>(type: "text", nullable: true),
                    gp_tel_no = table.Column<long>(type: "bigint", nullable: false),
                    gp_fax_no = table.Column<long>(type: "bigint", nullable: false),
                    gp_email_id = table.Column<string>(type: "text", nullable: true),
                    sychiatrist_name = table.Column<string>(type: "text", nullable: true),
                    sychiatrist_address = table.Column<string>(type: "text", nullable: true),
                    sychiatrist_tel_no = table.Column<long>(type: "bigint", nullable: false),
                    sychiatrist_fax_no = table.Column<long>(type: "bigint", nullable: false),
                    sychiatrist_email_id = table.Column<string>(type: "text", nullable: true),
                    other_significant_information = table.Column<string>(type: "text", nullable: true),
                    diagnosis = table.Column<string>(type: "text", nullable: true),
                    reference = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    last_updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistories", x => x.id);
                    table.ForeignKey(
                        name: "FK_MedicalHistories_ServiceUsers_service_user_id",
                        column: x => x.service_user_id,
                        principalTable: "ServiceUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalHistories_Users_created_by",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "reference", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("bb2d356b-9521-43e0-975a-3ef48e38123b"), null, new DateTimeOffset(new DateTime(2021, 8, 6, 12, 56, 35, 384, DateTimeKind.Unspecified).AddTicks(3692), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", new Guid("8b8f194e-896d-41b2-b5bb-c07f3a5ea98c"), null, null, 1 },
                    { new Guid("d42d67b5-75d0-4144-9dd8-fb5f70d4ea58"), null, new DateTimeOffset(new DateTime(2021, 8, 6, 12, 56, 35, 384, DateTimeKind.Unspecified).AddTicks(5002), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", new Guid("3b8e171a-e863-469d-999c-f17072f331e2"), null, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audits_created_by",
                table: "Audits",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_created_by",
                table: "MedicalHistories",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistories_service_user_id",
                table: "MedicalHistories",
                column: "service_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Residences_created_by",
                table: "Residences",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUsers_created_by",
                table: "ServiceUsers",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUsers_residence_id",
                table: "ServiceUsers",
                column: "residence_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_created_by",
                table: "Users",
                column: "created_by");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "MedicalHistories");

            migrationBuilder.DropTable(
                name: "ServiceUsers");

            migrationBuilder.DropTable(
                name: "Residences");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
