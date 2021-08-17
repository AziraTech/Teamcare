using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class dataTypechangesAdmissionDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("00ee65a2-984e-437d-8081-550052014a94"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("980ea54c-82e7-49d8-bfcf-cbdce023fe86"));

            migrationBuilder.DropColumn(
            name: "date_of_admission",
            table: "ServiceUsers");

            migrationBuilder.AddColumn<int>(
                name: "date_of_admission",
                table: "ServiceUsers",
                nullable: false,
                type: "timestamp without time zone",
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("ef466c47-483c-4f7b-b917-10899823074e"), null, new DateTimeOffset(new DateTime(2021, 8, 16, 13, 33, 46, 500, DateTimeKind.Unspecified).AddTicks(3910), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, 1 },
                    { new Guid("3928eeab-7e18-464e-ae09-812889d74b70"), null, new DateTimeOffset(new DateTime(2021, 8, 16, 13, 33, 46, 500, DateTimeKind.Unspecified).AddTicks(4862), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("3928eeab-7e18-464e-ae09-812889d74b70"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("ef466c47-483c-4f7b-b917-10899823074e"));

            migrationBuilder.DropColumn(
            name: "date_of_admission",
            table: "ServiceUsers");

            migrationBuilder.AddColumn<string>(
                name: "date_of_admission",
                table: "ServiceUsers",
                nullable: true
                );


            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("980ea54c-82e7-49d8-bfcf-cbdce023fe86"), null, new DateTimeOffset(new DateTime(2021, 8, 16, 12, 33, 1, 151, DateTimeKind.Unspecified).AddTicks(7088), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, 1 },
                    { new Guid("00ee65a2-984e-437d-8081-550052014a94"), null, new DateTimeOffset(new DateTime(2021, 8, 16, 12, 33, 1, 151, DateTimeKind.Unspecified).AddTicks(8033), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, 1 }
                });
        }
    }
}
