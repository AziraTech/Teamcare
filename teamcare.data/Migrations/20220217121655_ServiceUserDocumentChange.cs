using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class ServiceUserDocumentChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("c3ddcc96-6fed-4dd1-8087-f5b406111f91"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("e54bb57b-4e4b-4b7e-b3cd-5b4306bfdee7"));

            migrationBuilder.AddColumn<bool>(
                name: "is_confidential",
                table: "ServiceUserDocuments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("0384fbbb-80db-417c-ab8d-a5b014024d4d"), null, null, new DateTimeOffset(new DateTime(2022, 2, 17, 12, 16, 54, 28, DateTimeKind.Unspecified).AddTicks(1839), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 1, null, null, 1 },
                    { new Guid("e34af8cf-7a12-47cc-9bfa-8189b673f54e"), null, null, new DateTimeOffset(new DateTime(2022, 2, 17, 12, 16, 54, 28, DateTimeKind.Unspecified).AddTicks(4269), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 4, null, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("0384fbbb-80db-417c-ab8d-a5b014024d4d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("e34af8cf-7a12-47cc-9bfa-8189b673f54e"));

            migrationBuilder.DropColumn(
                name: "is_confidential",
                table: "ServiceUserDocuments");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("e54bb57b-4e4b-4b7e-b3cd-5b4306bfdee7"), null, null, new DateTimeOffset(new DateTime(2022, 2, 3, 12, 50, 54, 641, DateTimeKind.Unspecified).AddTicks(5728), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 1, null, null, 1 },
                    { new Guid("c3ddcc96-6fed-4dd1-8087-f5b406111f91"), null, null, new DateTimeOffset(new DateTime(2022, 2, 3, 12, 50, 54, 641, DateTimeKind.Unspecified).AddTicks(7190), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 4, null, null, 1 }
                });
        }
    }
}
