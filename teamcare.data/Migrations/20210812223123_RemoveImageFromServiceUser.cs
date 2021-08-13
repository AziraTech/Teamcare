using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class RemoveImageFromServiceUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("9f88835c-06d1-42d6-b530-1f04313a0b14"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("a68ec5e5-dde3-4de1-9dff-1ca13e541228"));

            migrationBuilder.DropColumn(
                name: "user_image",
                table: "ServiceUsers");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("8e224902-bd72-4574-a78e-843a105bb32c"), null, new DateTimeOffset(new DateTime(2021, 8, 12, 22, 31, 22, 858, DateTimeKind.Unspecified).AddTicks(1012), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, 1 },
                    { new Guid("b411c1e1-8877-4def-bc00-2349324c969d"), null, new DateTimeOffset(new DateTime(2021, 8, 12, 22, 31, 22, 858, DateTimeKind.Unspecified).AddTicks(3244), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("8e224902-bd72-4574-a78e-843a105bb32c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("b411c1e1-8877-4def-bc00-2349324c969d"));

            migrationBuilder.AddColumn<byte[]>(
                name: "user_image",
                table: "ServiceUsers",
                type: "bytea",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("9f88835c-06d1-42d6-b530-1f04313a0b14"), null, new DateTimeOffset(new DateTime(2021, 8, 12, 16, 14, 21, 228, DateTimeKind.Unspecified).AddTicks(555), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, 1 },
                    { new Guid("a68ec5e5-dde3-4de1-9dff-1ca13e541228"), null, new DateTimeOffset(new DateTime(2021, 8, 12, 16, 14, 21, 228, DateTimeKind.Unspecified).AddTicks(3711), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, 1 }
                });
        }
    }
}
