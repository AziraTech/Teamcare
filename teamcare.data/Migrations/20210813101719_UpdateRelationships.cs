using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class UpdateRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("8e224902-bd72-4574-a78e-843a105bb32c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("b411c1e1-8877-4def-bc00-2349324c969d"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("185fb9d9-8c8c-4efe-9b9d-00d8f0c166d1"), null, new DateTimeOffset(new DateTime(2021, 8, 13, 10, 17, 18, 590, DateTimeKind.Unspecified).AddTicks(1644), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, 1 },
                    { new Guid("f3f23af1-3275-4141-8b0b-1ae283505651"), null, new DateTimeOffset(new DateTime(2021, 8, 13, 10, 17, 18, 590, DateTimeKind.Unspecified).AddTicks(4007), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("185fb9d9-8c8c-4efe-9b9d-00d8f0c166d1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("f3f23af1-3275-4141-8b0b-1ae283505651"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("8e224902-bd72-4574-a78e-843a105bb32c"), null, new DateTimeOffset(new DateTime(2021, 8, 12, 22, 31, 22, 858, DateTimeKind.Unspecified).AddTicks(1012), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, 1 },
                    { new Guid("b411c1e1-8877-4def-bc00-2349324c969d"), null, new DateTimeOffset(new DateTime(2021, 8, 12, 22, 31, 22, 858, DateTimeKind.Unspecified).AddTicks(3244), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, 1 }
                });
        }
    }
}
