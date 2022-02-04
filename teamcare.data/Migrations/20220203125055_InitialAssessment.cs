using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class InitialAssessment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("12d163b1-24b9-449b-9d64-09813db2c27d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("b0c78856-189b-4b68-86d5-cd0ae71d8adc"));

            migrationBuilder.AddColumn<string>(
                name: "initial_assessment_content",
                table: "AssessmentSkills",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "initial_assessment_section",
                table: "AssessmentSkills",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("e54bb57b-4e4b-4b7e-b3cd-5b4306bfdee7"), null, null, new DateTimeOffset(new DateTime(2022, 2, 3, 12, 50, 54, 641, DateTimeKind.Unspecified).AddTicks(5728), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 1, null, null, 1 },
                    { new Guid("c3ddcc96-6fed-4dd1-8087-f5b406111f91"), null, null, new DateTimeOffset(new DateTime(2022, 2, 3, 12, 50, 54, 641, DateTimeKind.Unspecified).AddTicks(7190), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 4, null, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("c3ddcc96-6fed-4dd1-8087-f5b406111f91"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("e54bb57b-4e4b-4b7e-b3cd-5b4306bfdee7"));

            migrationBuilder.DropColumn(
                name: "initial_assessment_content",
                table: "AssessmentSkills");

            migrationBuilder.DropColumn(
                name: "initial_assessment_section",
                table: "AssessmentSkills");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("12d163b1-24b9-449b-9d64-09813db2c27d"), null, null, new DateTimeOffset(new DateTime(2022, 1, 27, 13, 42, 29, 537, DateTimeKind.Unspecified).AddTicks(5734), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 1, null, null, 1 },
                    { new Guid("b0c78856-189b-4b68-86d5-cd0ae71d8adc"), null, null, new DateTimeOffset(new DateTime(2022, 1, 27, 13, 42, 29, 537, DateTimeKind.Unspecified).AddTicks(7431), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 4, null, null, 1 }
                });
        }
    }
}
