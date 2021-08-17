using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class UpdateFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("144f258e-c752-45cb-802d-9e5895b9990c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("76760cce-cae0-4169-940e-78fdd92ff3d2"));

            migrationBuilder.AddColumn<string>(
                name: "content_type",
                table: "DocumentUploads",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "file_name",
                table: "DocumentUploads",
                type: "text",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "content_type",
                table: "DocumentUploads");

            migrationBuilder.DropColumn(
                name: "file_name",
                table: "DocumentUploads");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("76760cce-cae0-4169-940e-78fdd92ff3d2"), null, new DateTimeOffset(new DateTime(2021, 8, 12, 15, 47, 45, 658, DateTimeKind.Unspecified).AddTicks(4902), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, 1 },
                    { new Guid("144f258e-c752-45cb-802d-9e5895b9990c"), null, new DateTimeOffset(new DateTime(2021, 8, 12, 15, 47, 45, 658, DateTimeKind.Unspecified).AddTicks(7560), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, 1 }
                });
        }
    }
}
