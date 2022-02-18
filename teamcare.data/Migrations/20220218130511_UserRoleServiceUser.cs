using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class UserRoleServiceUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceUsers_created_by",
                table: "ServiceUsers");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("0384fbbb-80db-417c-ab8d-a5b014024d4d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("e34af8cf-7a12-47cc-9bfa-8189b673f54e"));

            migrationBuilder.AddColumn<Guid>(
                name: "service_user_id",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "service_user_id", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("1f59e32b-7c4d-4e52-b2c8-99b47916526d"), null, null, new DateTimeOffset(new DateTime(2022, 2, 18, 13, 5, 10, 290, DateTimeKind.Unspecified).AddTicks(2456), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, 1, null, null, 1 },
                    { new Guid("0cb1e6ac-667b-435d-b5dd-6ec0ec900e76"), null, null, new DateTimeOffset(new DateTime(2022, 2, 18, 13, 5, 10, 290, DateTimeKind.Unspecified).AddTicks(3802), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, 4, null, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_service_user_id",
                table: "Users",
                column: "service_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUsers_created_by",
                table: "ServiceUsers",
                column: "created_by",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ServiceUsers_service_user_id",
                table: "Users",
                column: "service_user_id",
                principalTable: "ServiceUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ServiceUsers_service_user_id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_service_user_id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ServiceUsers_created_by",
                table: "ServiceUsers");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("0cb1e6ac-667b-435d-b5dd-6ec0ec900e76"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("1f59e32b-7c4d-4e52-b2c8-99b47916526d"));

            migrationBuilder.DropColumn(
                name: "service_user_id",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("0384fbbb-80db-417c-ab8d-a5b014024d4d"), null, null, new DateTimeOffset(new DateTime(2022, 2, 17, 12, 16, 54, 28, DateTimeKind.Unspecified).AddTicks(1839), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 1, null, null, 1 },
                    { new Guid("e34af8cf-7a12-47cc-9bfa-8189b673f54e"), null, null, new DateTimeOffset(new DateTime(2022, 2, 17, 12, 16, 54, 28, DateTimeKind.Unspecified).AddTicks(4269), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 4, null, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUsers_created_by",
                table: "ServiceUsers",
                column: "created_by");
        }
    }
}
