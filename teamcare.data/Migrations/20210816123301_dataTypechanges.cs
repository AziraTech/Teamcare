using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class dataTypechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("185fb9d9-8c8c-4efe-9b9d-00d8f0c166d1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("f3f23af1-3275-4141-8b0b-1ae283505651"));


            migrationBuilder.DropColumn(
            name: "document_type",
            table: "DocumentUploads");

            migrationBuilder.AddColumn<int>(
                name: "document_type",
                table: "DocumentUploads",
                nullable: false,
                type:"integer",
                defaultValue:0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("980ea54c-82e7-49d8-bfcf-cbdce023fe86"), null, new DateTimeOffset(new DateTime(2021, 8, 16, 12, 33, 1, 151, DateTimeKind.Unspecified).AddTicks(7088), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, 1 },
                    { new Guid("00ee65a2-984e-437d-8081-550052014a94"), null, new DateTimeOffset(new DateTime(2021, 8, 16, 12, 33, 1, 151, DateTimeKind.Unspecified).AddTicks(8033), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
            name: "document_type",
            table: "DocumentUploads");

            migrationBuilder.AddColumn<string>(
                name: "document_type",
                table: "DocumentUploads",
                nullable: true,
                type: "text");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("185fb9d9-8c8c-4efe-9b9d-00d8f0c166d1"), null, new DateTimeOffset(new DateTime(2021, 8, 13, 10, 17, 18, 590, DateTimeKind.Unspecified).AddTicks(1644), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, 1 },
                    { new Guid("f3f23af1-3275-4141-8b0b-1ae283505651"), null, new DateTimeOffset(new DateTime(2021, 8, 13, 10, 17, 18, 590, DateTimeKind.Unspecified).AddTicks(4007), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, 1 }
                });
        }
    }
}
