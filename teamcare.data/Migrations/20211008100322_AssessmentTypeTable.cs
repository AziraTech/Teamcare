using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class AssessmentTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("50cb9e01-12ae-456f-ad0b-c52bb6c3e1c1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("6b886751-9c18-4ec5-bb2e-046178d2abbc"));

            migrationBuilder.DropColumn(
                name: "assessment_optionsgroup",
                table: "SkillGroups");

            migrationBuilder.DropColumn(
                name: "assessment_type",
                table: "SkillGroups");

            migrationBuilder.DropColumn(
                name: "assessment_type",
                table: "Assessments");

            migrationBuilder.AddColumn<Guid>(
                name: "assessment_type_id",
                table: "SkillGroups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "assessment_type_id",
                table: "Assessments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AssessmentTypes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type_name = table.Column<string>(type: "text", nullable: false),
                    options_group = table.Column<int>(type: "integer", nullable: false),
                    position = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    last_updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssessmentTypes", x => x.id);
                    table.ForeignKey(
                        name: "FK_AssessmentTypes_Users_created_by",
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
                    { new Guid("ebc90c82-ba40-453b-a4de-324089df2b22"), null, null, new DateTimeOffset(new DateTime(2021, 10, 8, 10, 3, 21, 547, DateTimeKind.Unspecified).AddTicks(2400), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 0, null, null, 1 },
                    { new Guid("a258c184-c0f0-4f3b-ba8d-87bf934ca593"), null, null, new DateTimeOffset(new DateTime(2021, 10, 8, 10, 3, 21, 547, DateTimeKind.Unspecified).AddTicks(4751), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 0, null, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkillGroups_assessment_type_id",
                table: "SkillGroups",
                column: "assessment_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_assessment_type_id",
                table: "Assessments",
                column: "assessment_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_AssessmentTypes_created_by",
                table: "AssessmentTypes",
                column: "created_by");

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_AssessmentTypes_assessment_type_id",
                table: "Assessments",
                column: "assessment_type_id",
                principalTable: "AssessmentTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SkillGroups_AssessmentTypes_assessment_type_id",
                table: "SkillGroups",
                column: "assessment_type_id",
                principalTable: "AssessmentTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_AssessmentTypes_assessment_type_id",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_SkillGroups_AssessmentTypes_assessment_type_id",
                table: "SkillGroups");

            migrationBuilder.DropTable(
                name: "AssessmentTypes");

            migrationBuilder.DropIndex(
                name: "IX_SkillGroups_assessment_type_id",
                table: "SkillGroups");

            migrationBuilder.DropIndex(
                name: "IX_Assessments_assessment_type_id",
                table: "Assessments");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("a258c184-c0f0-4f3b-ba8d-87bf934ca593"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("ebc90c82-ba40-453b-a4de-324089df2b22"));

            migrationBuilder.DropColumn(
                name: "assessment_type_id",
                table: "SkillGroups");

            migrationBuilder.DropColumn(
                name: "assessment_type_id",
                table: "Assessments");

            migrationBuilder.AddColumn<int>(
                name: "assessment_optionsgroup",
                table: "SkillGroups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "assessment_type",
                table: "SkillGroups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "assessment_type",
                table: "Assessments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("6b886751-9c18-4ec5-bb2e-046178d2abbc"), null, null, new DateTimeOffset(new DateTime(2021, 10, 7, 9, 29, 20, 498, DateTimeKind.Unspecified).AddTicks(6186), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 0, null, null, 1 },
                    { new Guid("50cb9e01-12ae-456f-ad0b-c52bb6c3e1c1"), null, null, new DateTimeOffset(new DateTime(2021, 10, 7, 9, 29, 20, 499, DateTimeKind.Unspecified).AddTicks(2695), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 0, null, null, 1 }
                });
        }
    }
}
