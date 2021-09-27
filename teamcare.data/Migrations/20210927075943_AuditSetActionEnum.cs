using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class AuditSetActionEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("1e85a93b-9571-4ca5-b246-0d658f2fc25d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("ce9a7ff8-eedb-46cd-80b9-ab9569df0413"));


            migrationBuilder.DropColumn(
         name: "action",
         table: "Audits");

            migrationBuilder.AddColumn<int>(
                name: "action",
                table: "Audits",
                nullable: false,
                type: "integer",
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("492875ef-ae2d-4de0-9339-e2982df7de14"), null, null, new DateTimeOffset(new DateTime(2021, 9, 27, 7, 59, 40, 960, DateTimeKind.Unspecified).AddTicks(6369), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 0, null, null, 1 },
                    { new Guid("9cbd34f3-df4f-43fe-b0f4-0fe06428f252"), null, null, new DateTimeOffset(new DateTime(2021, 9, 27, 7, 59, 40, 961, DateTimeKind.Unspecified).AddTicks(1235), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 0, null, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("492875ef-ae2d-4de0-9339-e2982df7de14"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("9cbd34f3-df4f-43fe-b0f4-0fe06428f252"));


            migrationBuilder.DropColumn(
          name: "action",
          table: "Audits");

            migrationBuilder.AddColumn<string>(
                name: "action",
                table: "Audits",
                nullable: true,
                type: "text");


            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("1e85a93b-9571-4ca5-b246-0d658f2fc25d"), null, null, new DateTimeOffset(new DateTime(2021, 9, 24, 13, 10, 48, 62, DateTimeKind.Unspecified).AddTicks(7798), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 0, null, null, 1 },
                    { new Guid("ce9a7ff8-eedb-46cd-80b9-ab9569df0413"), null, null, new DateTimeOffset(new DateTime(2021, 9, 24, 13, 10, 48, 62, DateTimeKind.Unspecified).AddTicks(9628), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 0, null, null, 1 }
                });
        }
    }
}
