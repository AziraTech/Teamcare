using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class AssessmentTypeToGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("71468aff-3aa7-4ea5-8320-3c620f883152"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("9ab1defa-5b9f-418e-9120-7973cd78445e"));

            migrationBuilder.AddColumn<int>(
                name: "assessment_type",
                table: "SkillGroups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("124a8175-388c-45ce-853d-a6fabe25831b"), null, null, new DateTimeOffset(new DateTime(2021, 9, 17, 9, 16, 47, 542, DateTimeKind.Unspecified).AddTicks(9151), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 0, null, null, 1 },
                    { new Guid("3ab28e10-ae92-4a2c-84af-2155be61a74d"), null, null, new DateTimeOffset(new DateTime(2021, 9, 17, 9, 16, 47, 543, DateTimeKind.Unspecified).AddTicks(1653), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 0, null, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("124a8175-388c-45ce-853d-a6fabe25831b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("3ab28e10-ae92-4a2c-84af-2155be61a74d"));

            migrationBuilder.DropColumn(
                name: "assessment_type",
                table: "SkillGroups");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("71468aff-3aa7-4ea5-8320-3c620f883152"), null, null, new DateTimeOffset(new DateTime(2021, 9, 15, 8, 17, 27, 809, DateTimeKind.Unspecified).AddTicks(9262), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 0, null, null, 1 },
                    { new Guid("9ab1defa-5b9f-418e-9120-7973cd78445e"), null, null, new DateTimeOffset(new DateTime(2021, 9, 15, 8, 17, 27, 810, DateTimeKind.Unspecified).AddTicks(363), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 0, null, null, 1 }
                });
        }
    }
}
