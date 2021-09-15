using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class AssessmentAndAssessmentSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("68d2d0ec-e493-40a9-a0a0-bf4c32e27140"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("e8e53130-73ab-41eb-8d1d-d2b52e1ba5cd"));

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    assessment_type = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    last_updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Assessments_ServiceUsers_service_user_id",
                        column: x => x.service_user_id,
                        principalTable: "ServiceUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessments_Users_created_by",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssessmentSkills",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    assessment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    skill_id = table.Column<Guid>(type: "uuid", nullable: false),
                    skill_group = table.Column<string>(type: "text", nullable: true),
                    skill_name = table.Column<string>(type: "text", nullable: true),
                    skill_level = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    last_updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentSkills", x => x.id);
                    table.ForeignKey(
                        name: "FK_AssessmentSkills_Assessments_assessment_id",
                        column: x => x.assessment_id,
                        principalTable: "Assessments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentSkills_LivingSkills_skill_id",
                        column: x => x.skill_id,
                        principalTable: "LivingSkills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssessmentSkills_Users_created_by",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("71468aff-3aa7-4ea5-8320-3c620f883152"), null, null, new DateTimeOffset(new DateTime(2021, 9, 15, 8, 17, 27, 809, DateTimeKind.Unspecified).AddTicks(9262), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 0, null, null, 1 },
                    { new Guid("9ab1defa-5b9f-418e-9120-7973cd78445e"), null, null, new DateTimeOffset(new DateTime(2021, 9, 15, 8, 17, 27, 810, DateTimeKind.Unspecified).AddTicks(363), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 0, null, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_created_by",
                table: "Assessments",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_service_user_id",
                table: "Assessments",
                column: "service_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSkills_assessment_id",
                table: "AssessmentSkills",
                column: "assessment_id");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSkills_created_by",
                table: "AssessmentSkills",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentSkills_skill_id",
                table: "AssessmentSkills",
                column: "skill_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssessmentSkills");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("71468aff-3aa7-4ea5-8320-3c620f883152"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("9ab1defa-5b9f-418e-9120-7973cd78445e"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("68d2d0ec-e493-40a9-a0a0-bf4c32e27140"), null, null, new DateTimeOffset(new DateTime(2021, 9, 10, 8, 19, 38, 470, DateTimeKind.Unspecified).AddTicks(3483), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 0, null, null, 1 },
                    { new Guid("e8e53130-73ab-41eb-8d1d-d2b52e1ba5cd"), null, null, new DateTimeOffset(new DateTime(2021, 9, 10, 8, 19, 38, 470, DateTimeKind.Unspecified).AddTicks(4777), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 0, null, null, 1 }
                });
        }
    }
}
