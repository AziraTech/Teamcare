using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class ServiceUserLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("4844c5f4-e04c-4b69-ba87-49ee06575f95"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("f40a5328-9151-47ad-aa72-c3b9818dd503"));

            migrationBuilder.CreateTable(
                name: "ServiceUserLog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    log_created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    log_created_for = table.Column<Guid>(type: "uuid", nullable: false),
                    log_message = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    last_updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceUserLog", x => x.id);
                    table.ForeignKey(
                        name: "FK_ServiceUserLog_ServiceUsers_log_created_for",
                        column: x => x.log_created_for,
                        principalTable: "ServiceUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceUserLog_Users_created_by",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceUserLog_Users_log_created_by",
                        column: x => x.log_created_by,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("48609336-f3a1-4f5a-9ded-905974f91293"), null, null, new DateTimeOffset(new DateTime(2021, 8, 31, 10, 40, 5, 957, DateTimeKind.Unspecified).AddTicks(8442), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, null, null, 1 },
                    { new Guid("177d00f9-874b-4334-981f-9087dc7364fa"), null, null, new DateTimeOffset(new DateTime(2021, 8, 31, 10, 40, 5, 958, DateTimeKind.Unspecified).AddTicks(504), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, null, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUserLog_created_by",
                table: "ServiceUserLog",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUserLog_log_created_by",
                table: "ServiceUserLog",
                column: "log_created_by");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUserLog_log_created_for",
                table: "ServiceUserLog",
                column: "log_created_for");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceUserLog");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("177d00f9-874b-4334-981f-9087dc7364fa"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("48609336-f3a1-4f5a-9ded-905974f91293"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("f40a5328-9151-47ad-aa72-c3b9818dd503"), null, null, new DateTimeOffset(new DateTime(2021, 8, 31, 10, 38, 15, 573, DateTimeKind.Unspecified).AddTicks(282), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, null, null, 1 },
                    { new Guid("4844c5f4-e04c-4b69-ba87-49ee06575f95"), null, null, new DateTimeOffset(new DateTime(2021, 8, 31, 10, 38, 15, 573, DateTimeKind.Unspecified).AddTicks(3119), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, null, null, 1 }
                });
        }
    }
}
