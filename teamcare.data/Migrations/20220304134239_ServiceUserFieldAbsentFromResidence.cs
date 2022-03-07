using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class ServiceUserFieldAbsentFromResidence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("ce3c8b44-0fe3-428a-8358-be7b23423e58"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("d237dbf6-3aa1-4d09-b93c-7390d61ddfc1"));

            migrationBuilder.AddColumn<bool>(
                name: "absent_from_residence",
                table: "ServiceUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "service_user_id", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("d8aae711-1372-4aed-bf5f-c18683f8b44c"), null, null, new DateTimeOffset(new DateTime(2022, 3, 4, 13, 42, 38, 591, DateTimeKind.Unspecified).AddTicks(5802), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, 1, null, null, 1 },
                    { new Guid("1eb38aff-dcda-42c6-89dc-e19bd6f87249"), null, null, new DateTimeOffset(new DateTime(2022, 3, 4, 13, 42, 38, 591, DateTimeKind.Unspecified).AddTicks(8125), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, 4, null, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("1eb38aff-dcda-42c6-89dc-e19bd6f87249"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("d8aae711-1372-4aed-bf5f-c18683f8b44c"));

            migrationBuilder.DropColumn(
                name: "absent_from_residence",
                table: "ServiceUsers");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "service_user_id", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("d237dbf6-3aa1-4d09-b93c-7390d61ddfc1"), null, null, new DateTimeOffset(new DateTime(2022, 3, 3, 9, 37, 36, 378, DateTimeKind.Unspecified).AddTicks(2136), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, 1, null, null, 1 },
                    { new Guid("ce3c8b44-0fe3-428a-8358-be7b23423e58"), null, null, new DateTimeOffset(new DateTime(2022, 3, 3, 9, 37, 36, 378, DateTimeKind.Unspecified).AddTicks(3382), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, 4, null, null, 1 }
                });
        }
    }
}
