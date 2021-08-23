using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class ServiceUserArchiveFunctions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("26fe31e7-53ac-4f06-8b51-2313d867f70e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("cf0f483d-aebf-4cb8-baaa-98810e9f1317"));

            migrationBuilder.AddColumn<DateTime>(
                name: "archived_on",
                table: "ServiceUsers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "archived_reason",
                table: "ServiceUsers",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("b6b3c284-f898-4fc0-8424-1d28e1e174dc"), null, null, new DateTimeOffset(new DateTime(2021, 8, 23, 5, 44, 0, 999, DateTimeKind.Unspecified).AddTicks(4183), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, null, null, 1 },
                    { new Guid("3dd61635-81ad-4f82-9c26-61b8362be37e"), null, null, new DateTimeOffset(new DateTime(2021, 8, 23, 5, 44, 0, 999, DateTimeKind.Unspecified).AddTicks(5447), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, null, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("3dd61635-81ad-4f82-9c26-61b8362be37e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("b6b3c284-f898-4fc0-8424-1d28e1e174dc"));

            migrationBuilder.DropColumn(
                name: "archived_on",
                table: "ServiceUsers");

            migrationBuilder.DropColumn(
                name: "archived_reason",
                table: "ServiceUsers");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("26fe31e7-53ac-4f06-8b51-2313d867f70e"), null, null, new DateTimeOffset(new DateTime(2021, 8, 18, 21, 28, 56, 385, DateTimeKind.Unspecified).AddTicks(1911), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, null, null, null, null, 1 },
                    { new Guid("cf0f483d-aebf-4cb8-baaa-98810e9f1317"), null, null, new DateTimeOffset(new DateTime(2021, 8, 18, 21, 28, 56, 385, DateTimeKind.Unspecified).AddTicks(4287), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, null, null, null, null, 1 }
                });
        }
    }
}
