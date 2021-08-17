using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class ReferenceFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("bb2d356b-9521-43e0-975a-3ef48e38123b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("d42d67b5-75d0-4144-9dd8-fb5f70d4ea58"));

            migrationBuilder.DropColumn(
                name: "reference",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "reference",
                table: "ServiceUsers");

            migrationBuilder.DropColumn(
                name: "reference",
                table: "Residences");

            migrationBuilder.DropColumn(
                name: "reference",
                table: "MedicalHistories");

            migrationBuilder.DropColumn(
                name: "reference",
                table: "Audits");

            migrationBuilder.RenameColumn(
                name: "prefered_first_language",
                table: "ServiceUsers",
                newName: "preferred_first_language");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("5320f930-2353-47f8-a656-eeb3c37e0019"), null, new DateTimeOffset(new DateTime(2021, 8, 7, 10, 23, 25, 485, DateTimeKind.Unspecified).AddTicks(4746), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, 1 },
                    { new Guid("4136669d-7ee0-4983-a168-78e3a9738611"), null, new DateTimeOffset(new DateTime(2021, 8, 7, 10, 23, 25, 485, DateTimeKind.Unspecified).AddTicks(7007), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("4136669d-7ee0-4983-a168-78e3a9738611"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("5320f930-2353-47f8-a656-eeb3c37e0019"));

            migrationBuilder.RenameColumn(
                name: "preferred_first_language",
                table: "ServiceUsers",
                newName: "prefered_first_language");

            migrationBuilder.AddColumn<Guid>(
                name: "reference",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "reference",
                table: "ServiceUsers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "reference",
                table: "Residences",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "reference",
                table: "MedicalHistories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "reference",
                table: "Audits",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "reference", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("bb2d356b-9521-43e0-975a-3ef48e38123b"), null, new DateTimeOffset(new DateTime(2021, 8, 6, 12, 56, 35, 384, DateTimeKind.Unspecified).AddTicks(3692), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", new Guid("8b8f194e-896d-41b2-b5bb-c07f3a5ea98c"), null, null, 1 },
                    { new Guid("d42d67b5-75d0-4144-9dd8-fb5f70d4ea58"), null, new DateTimeOffset(new DateTime(2021, 8, 6, 12, 56, 35, 384, DateTimeKind.Unspecified).AddTicks(5002), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", new Guid("3b8e171a-e863-469d-999c-f17072f331e2"), null, null, 1 }
                });
        }
    }
}
