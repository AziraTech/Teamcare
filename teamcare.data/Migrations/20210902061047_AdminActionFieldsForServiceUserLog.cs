using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class AdminActionFieldsForServiceUserLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceUserLog_Users_log_created_by",
                table: "ServiceUserLog");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("177d00f9-874b-4334-981f-9087dc7364fa"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("48609336-f3a1-4f5a-9ded-905974f91293"));

            migrationBuilder.RenameColumn(
                name: "log_created_by",
                table: "ServiceUserLog",
                newName: "action_by_admin_id");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceUserLog_log_created_by",
                table: "ServiceUserLog",
                newName: "IX_ServiceUserLog_action_by_admin_id");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "admin_action_on",
                table: "ServiceUserLog",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_approved",
                table: "ServiceUserLog",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_visible",
                table: "ServiceUserLog",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "log_message_updated",
                table: "ServiceUserLog",
                type: "text",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "admin_action_on",
                table: "ServiceUserLog");

            migrationBuilder.DropColumn(
                name: "is_approved",
                table: "ServiceUserLog");

            migrationBuilder.DropColumn(
                name: "is_visible",
                table: "ServiceUserLog");

            migrationBuilder.DropColumn(
                name: "log_message_updated",
                table: "ServiceUserLog");

            migrationBuilder.RenameColumn(
                name: "action_by_admin_id",
                table: "ServiceUserLog",
                newName: "log_created_by");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceUserLog_action_by_admin_id",
                table: "ServiceUserLog",
                newName: "IX_ServiceUserLog_log_created_by");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("48609336-f3a1-4f5a-9ded-905974f91293"), null, null, new DateTimeOffset(new DateTime(2021, 8, 31, 10, 40, 5, 957, DateTimeKind.Unspecified).AddTicks(8442), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, null, null, 1 },
                    { new Guid("177d00f9-874b-4334-981f-9087dc7364fa"), null, null, new DateTimeOffset(new DateTime(2021, 8, 31, 10, 40, 5, 958, DateTimeKind.Unspecified).AddTicks(504), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, null, null, 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceUserLog_Users_log_created_by",
                table: "ServiceUserLog",
                column: "log_created_by",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
