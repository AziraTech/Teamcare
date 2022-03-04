using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class NewFieldServiceUserLogCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("0cb1e6ac-667b-435d-b5dd-6ec0ec900e76"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("1f59e32b-7c4d-4e52-b2c8-99b47916526d"));

            migrationBuilder.AddColumn<int>(
                name: "log_category",
                table: "ServiceUserLog",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "service_user_id", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("d237dbf6-3aa1-4d09-b93c-7390d61ddfc1"), null, null, new DateTimeOffset(new DateTime(2022, 3, 3, 9, 37, 36, 378, DateTimeKind.Unspecified).AddTicks(2136), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, 1, null, null, 1 },
                    { new Guid("ce3c8b44-0fe3-428a-8358-be7b23423e58"), null, null, new DateTimeOffset(new DateTime(2022, 3, 3, 9, 37, 36, 378, DateTimeKind.Unspecified).AddTicks(3382), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, 4, null, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("ce3c8b44-0fe3-428a-8358-be7b23423e58"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("d237dbf6-3aa1-4d09-b93c-7390d61ddfc1"));

            migrationBuilder.DropColumn(
                name: "log_category",
                table: "ServiceUserLog");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "service_user_id", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("1f59e32b-7c4d-4e52-b2c8-99b47916526d"), null, null, new DateTimeOffset(new DateTime(2022, 2, 18, 13, 5, 10, 290, DateTimeKind.Unspecified).AddTicks(2456), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, 1, null, null, 1 },
                    { new Guid("0cb1e6ac-667b-435d-b5dd-6ec0ec900e76"), null, null, new DateTimeOffset(new DateTime(2022, 2, 18, 13, 5, 10, 290, DateTimeKind.Unspecified).AddTicks(3802), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, 4, null, null, 1 }
                });
        }
    }
}
