using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class NullableValueForServiceUserLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceUserLog_Users_action_by_admin_id",
                table: "ServiceUserLog");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("20b3c10c-6f41-41a2-984e-425b7217e0b9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("6a91c03f-1159-40fa-b52d-277d8db14542"));

            migrationBuilder.AlterColumn<Guid>(
                name: "action_by_admin_id",
                table: "ServiceUserLog",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("3e32a1c5-3de2-4335-a441-66c3eca9dc74"), null, null, new DateTimeOffset(new DateTime(2021, 9, 2, 13, 10, 42, 749, DateTimeKind.Unspecified).AddTicks(5049), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, null, null, 1 },
                    { new Guid("58773bd5-9b3e-45b9-8276-c421b5230c8b"), null, null, new DateTimeOffset(new DateTime(2021, 9, 2, 13, 10, 42, 749, DateTimeKind.Unspecified).AddTicks(6122), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, null, null, 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceUserLog_Users_action_by_admin_id",
                table: "ServiceUserLog",
                column: "action_by_admin_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceUserLog_Users_action_by_admin_id",
                table: "ServiceUserLog");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("3e32a1c5-3de2-4335-a441-66c3eca9dc74"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("58773bd5-9b3e-45b9-8276-c421b5230c8b"));

            migrationBuilder.AlterColumn<Guid>(
                name: "action_by_admin_id",
                table: "ServiceUserLog",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("20b3c10c-6f41-41a2-984e-425b7217e0b9"), null, null, new DateTimeOffset(new DateTime(2021, 9, 2, 6, 10, 47, 109, DateTimeKind.Unspecified).AddTicks(7134), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, null, null, 1 },
                    { new Guid("6a91c03f-1159-40fa-b52d-277d8db14542"), null, null, new DateTimeOffset(new DateTime(2021, 9, 2, 6, 10, 47, 109, DateTimeKind.Unspecified).AddTicks(8228), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, null, null, 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceUserLog_Users_action_by_admin_id",
                table: "ServiceUserLog",
                column: "action_by_admin_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
