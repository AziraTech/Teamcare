using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class WeightReadingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DeleteData(
				table: "Users",
				keyColumn: "id",
				keyValue: new Guid("ce48dc9d-e1ec-42d8-86e0-2c21900da2e1"));

			migrationBuilder.DeleteData(
				table: "Users",
				keyColumn: "id",
				keyValue: new Guid("e5dfe669-d7f7-40ec-9311-72528f90f9b4"));

			migrationBuilder.AlterColumn<DateTime>(
                name: "date_received",
                table: "ServiceUserDocuments",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "test_date",
                table: "BloodPressureReadings",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.CreateTable(
                name: "WeightReadings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    test_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    last_updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightReadings", x => x.id);
                    table.ForeignKey(
                        name: "FK_WeightReadings_ServiceUsers_service_user_id",
                        column: x => x.service_user_id,
                        principalTable: "ServiceUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeightReadings_Users_created_by",
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
					{ new Guid("65380170-702b-430f-b5b5-af88f34c0d2f"), null, null, new DateTimeOffset(new DateTime(2021, 10, 19, 8, 57, 10, 349, DateTimeKind.Unspecified).AddTicks(8089), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 1, null, null, 1 },
					{ new Guid("6f0ac5c2-f1c1-4564-80fb-cbf38c21f14e"), null, null, new DateTimeOffset(new DateTime(2021, 10, 19, 8, 57, 10, 349, DateTimeKind.Unspecified).AddTicks(9360), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 4, null, null, 1 }
				});

			migrationBuilder.CreateIndex(
                name: "IX_WeightReadings_created_by",
                table: "WeightReadings",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_WeightReadings_service_user_id",
                table: "WeightReadings",
                column: "service_user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeightReadings");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("65380170-702b-430f-b5b5-af88f34c0d2f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("6f0ac5c2-f1c1-4564-80fb-cbf38c21f14e"));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "date_received",
                table: "ServiceUserDocuments",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "test_date",
                table: "BloodPressureReadings",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("ce48dc9d-e1ec-42d8-86e0-2c21900da2e1"), null, null, new DateTimeOffset(new DateTime(2021, 10, 18, 7, 26, 24, 622, DateTimeKind.Unspecified).AddTicks(6015), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 1, null, null, 1 },
                    { new Guid("e5dfe669-d7f7-40ec-9311-72528f90f9b4"), null, null, new DateTimeOffset(new DateTime(2021, 10, 18, 7, 26, 24, 622, DateTimeKind.Unspecified).AddTicks(8017), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 4, null, null, 1 }
                });
        }
    }
}
