using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class ChangeTitleFieldOfUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("3e32a1c5-3de2-4335-a441-66c3eca9dc74"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("58773bd5-9b3e-45b9-8276-c421b5230c8b"));


            migrationBuilder.DropColumn(
            name: "title",
            table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "title",
                table: "Users",
                nullable: false,
                type: "integer",
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("9dcb0620-6d06-4ad4-99d6-9fbac1ffa019"), null, null, new DateTimeOffset(new DateTime(2021, 9, 7, 13, 6, 27, 108, DateTimeKind.Unspecified).AddTicks(6506), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 0, null, null, 1 },
                    { new Guid("325e1974-1e43-49e4-8689-18a2aadac4c0"), null, null, new DateTimeOffset(new DateTime(2021, 9, 7, 13, 6, 27, 108, DateTimeKind.Unspecified).AddTicks(7657), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 0, null, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("325e1974-1e43-49e4-8689-18a2aadac4c0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("9dcb0620-6d06-4ad4-99d6-9fbac1ffa019"));

            migrationBuilder.DropColumn(
           name: "title",
           table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "Users",
                nullable: true,
                type: "text");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("3e32a1c5-3de2-4335-a441-66c3eca9dc74"), null, null, new DateTimeOffset(new DateTime(2021, 9, 2, 13, 10, 42, 749, DateTimeKind.Unspecified).AddTicks(5049), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, null, null, 1 },
                    { new Guid("58773bd5-9b3e-45b9-8276-c421b5230c8b"), null, null, new DateTimeOffset(new DateTime(2021, 9, 2, 13, 10, 42, 749, DateTimeKind.Unspecified).AddTicks(6122), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, null, null, 1 }
                });
        }
    }
}
