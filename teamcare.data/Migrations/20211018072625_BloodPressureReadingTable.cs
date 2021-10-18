using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class BloodPressureReadingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("3708c8fd-e14a-4684-b15e-0002d1289f36"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("f6f064bd-a28b-4d62-97ab-78e50f1f0e3f"));

            migrationBuilder.CreateTable(
                name: "BloodPressureReadings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    test_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    systolic_reading = table.Column<int>(type: "integer", nullable: false),
                    diastolic_reading = table.Column<int>(type: "integer", nullable: false),
                    pulse = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    last_updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodPressureReadings", x => x.id);
                    table.ForeignKey(
                        name: "FK_BloodPressureReadings_ServiceUsers_service_user_id",
                        column: x => x.service_user_id,
                        principalTable: "ServiceUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BloodPressureReadings_Users_created_by",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("ce48dc9d-e1ec-42d8-86e0-2c21900da2e1"), null, null, new DateTimeOffset(new DateTime(2021, 10, 18, 7, 26, 24, 622, DateTimeKind.Unspecified).AddTicks(6015), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 1, null, null, 1 },
                    { new Guid("e5dfe669-d7f7-40ec-9311-72528f90f9b4"), null, null, new DateTimeOffset(new DateTime(2021, 10, 18, 7, 26, 24, 622, DateTimeKind.Unspecified).AddTicks(8017), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 4, null, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodPressureReadings_created_by",
                table: "BloodPressureReadings",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_BloodPressureReadings_service_user_id",
                table: "BloodPressureReadings",
                column: "service_user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodPressureReadings");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("ce48dc9d-e1ec-42d8-86e0-2c21900da2e1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("e5dfe669-d7f7-40ec-9311-72528f90f9b4"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("3708c8fd-e14a-4684-b15e-0002d1289f36"), null, null, new DateTimeOffset(new DateTime(2021, 10, 15, 9, 30, 31, 934, DateTimeKind.Unspecified).AddTicks(6675), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 1, null, null, 1 },
                    { new Guid("f6f064bd-a28b-4d62-97ab-78e50f1f0e3f"), null, null, new DateTimeOffset(new DateTime(2021, 10, 15, 9, 30, 31, 934, DateTimeKind.Unspecified).AddTicks(8116), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 4, null, null, 1 }
                });
        }
    }
}
