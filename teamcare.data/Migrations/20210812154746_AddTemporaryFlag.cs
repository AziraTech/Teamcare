using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class AddTemporaryFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("7fb974fc-6bcf-4302-b44b-f183d6f8ac1b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("88610cfd-235d-497c-9930-9d5d4630468e"));

            migrationBuilder.DropColumn(
                name: "file_path",
                table: "DocumentUploads");

            migrationBuilder.RenameColumn(
                name: "original_file_name",
                table: "DocumentUploads",
                newName: "blob_name");

            migrationBuilder.AddColumn<bool>(
                name: "is_temporary",
                table: "DocumentUploads",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("76760cce-cae0-4169-940e-78fdd92ff3d2"), null, new DateTimeOffset(new DateTime(2021, 8, 12, 15, 47, 45, 658, DateTimeKind.Unspecified).AddTicks(4902), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, 1 },
                    { new Guid("144f258e-c752-45cb-802d-9e5895b9990c"), null, new DateTimeOffset(new DateTime(2021, 8, 12, 15, 47, 45, 658, DateTimeKind.Unspecified).AddTicks(7560), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("144f258e-c752-45cb-802d-9e5895b9990c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("76760cce-cae0-4169-940e-78fdd92ff3d2"));

            migrationBuilder.DropColumn(
                name: "is_temporary",
                table: "DocumentUploads");

            migrationBuilder.RenameColumn(
                name: "blob_name",
                table: "DocumentUploads",
                newName: "original_file_name");

            migrationBuilder.AddColumn<string>(
                name: "file_path",
                table: "DocumentUploads",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("88610cfd-235d-497c-9930-9d5d4630468e"), null, new DateTimeOffset(new DateTime(2021, 8, 10, 21, 46, 15, 944, DateTimeKind.Unspecified).AddTicks(355), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, 1 },
                    { new Guid("7fb974fc-6bcf-4302-b44b-f183d6f8ac1b"), null, new DateTimeOffset(new DateTime(2021, 8, 10, 21, 46, 15, 944, DateTimeKind.Unspecified).AddTicks(3258), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, 1 }
                });
        }
    }
}
