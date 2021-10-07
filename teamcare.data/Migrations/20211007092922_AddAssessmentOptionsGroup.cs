using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class AddAssessmentOptionsGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("9ad2c96c-d6e8-4a45-8a44-497b5535534c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("e2012571-d6ca-414c-acfd-a19e6a7c9adf"));

            migrationBuilder.AddColumn<int>(
                name: "assessment_optionsgroup",
                table: "SkillGroups",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("9ad2c96c-d6e8-4a45-8a44-497b5535534c"), null, null, new DateTimeOffset(new DateTime(2021, 10, 5, 9, 45, 58, 570, DateTimeKind.Unspecified).AddTicks(9401), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 0, null, null, 1 },
                    { new Guid("e2012571-d6ca-414c-acfd-a19e6a7c9adf"), null, null, new DateTimeOffset(new DateTime(2021, 10, 5, 9, 45, 58, 571, DateTimeKind.Unspecified).AddTicks(4322), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 0, null, null, 1 }
                });
        }
    }
}
