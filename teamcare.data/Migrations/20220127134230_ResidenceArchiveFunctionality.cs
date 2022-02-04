using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class ResidenceArchiveFunctionality : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeightRecording");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("65380170-702b-430f-b5b5-af88f34c0d2f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("6f0ac5c2-f1c1-4564-80fb-cbf38c21f14e"));

            migrationBuilder.AddColumn<DateTime>(
                name: "archived_on",
                table: "Residences",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "archived_reason",
                table: "Residences",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("12d163b1-24b9-449b-9d64-09813db2c27d"), null, null, new DateTimeOffset(new DateTime(2022, 1, 27, 13, 42, 29, 537, DateTimeKind.Unspecified).AddTicks(5734), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 1, null, null, 1 },
                    { new Guid("b0c78856-189b-4b68-86d5-cd0ae71d8adc"), null, null, new DateTimeOffset(new DateTime(2022, 1, 27, 13, 42, 29, 537, DateTimeKind.Unspecified).AddTicks(7431), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 4, null, null, 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_WeightReadings_ServiceUsers_service_user_id",
                table: "WeightReadings",
                column: "service_user_id",
                principalTable: "ServiceUsers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeightReadings_Users_created_by",
                table: "WeightReadings",
                column: "created_by",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeightReadings_ServiceUsers_service_user_id",
                table: "WeightReadings");

            migrationBuilder.DropForeignKey(
                name: "FK_WeightReadings_Users_created_by",
                table: "WeightReadings");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("12d163b1-24b9-449b-9d64-09813db2c27d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("b0c78856-189b-4b68-86d5-cd0ae71d8adc"));

            migrationBuilder.DropColumn(
                name: "archived_on",
                table: "Residences");

            migrationBuilder.DropColumn(
                name: "archived_reason",
                table: "Residences");

            migrationBuilder.CreateTable(
                name: "WeightRecording",
                columns: table => new
                {
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ServiceUserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_WeightRecording_ServiceUsers_ServiceUserId",
                        column: x => x.ServiceUserId,
                        principalTable: "ServiceUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeightRecording_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("65380170-702b-430f-b5b5-af88f34c0d2f"), null, null, new DateTimeOffset(new DateTime(2021, 10, 19, 8, 57, 10, 349, DateTimeKind.Unspecified).AddTicks(8089), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 1, null, null, 1 },
                    { new Guid("6f0ac5c2-f1c1-4564-80fb-cbf38c21f14e"), null, null, new DateTimeOffset(new DateTime(2021, 10, 19, 8, 57, 10, 349, DateTimeKind.Unspecified).AddTicks(9360), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 4, null, null, 1 }
                });
        }
    }
}
