using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class ChangeMiscellaneousPoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentSkills_LivingSkills_skill_id",
                table: "AssessmentSkills");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("0b748e48-9852-4c8e-918e-bbc9d0ebbb8c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("e1142b3b-0cc5-4157-8929-4c329dc1a9e9"));

            migrationBuilder.DropColumn(
                name: "address",
                table: "ServiceUsers");

            migrationBuilder.DropColumn(
                name: "contact_details",
                table: "ServiceUsers");

            migrationBuilder.DropColumn(
                name: "next_of_kin",
                table: "ServiceUsers");

            migrationBuilder.DropColumn(
                name: "relationship_to_person",
                table: "ServiceUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "skill_id",
                table: "AssessmentSkills",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("1e85a93b-9571-4ca5-b246-0d658f2fc25d"), null, null, new DateTimeOffset(new DateTime(2021, 9, 24, 13, 10, 48, 62, DateTimeKind.Unspecified).AddTicks(7798), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 0, null, null, 1 },
                    { new Guid("ce9a7ff8-eedb-46cd-80b9-ab9569df0413"), null, null, new DateTimeOffset(new DateTime(2021, 9, 24, 13, 10, 48, 62, DateTimeKind.Unspecified).AddTicks(9628), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 0, null, null, 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentSkills_LivingSkills_skill_id",
                table: "AssessmentSkills",
                column: "skill_id",
                principalTable: "LivingSkills",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssessmentSkills_LivingSkills_skill_id",
                table: "AssessmentSkills");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("1e85a93b-9571-4ca5-b246-0d658f2fc25d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("ce9a7ff8-eedb-46cd-80b9-ab9569df0413"));

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "ServiceUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contact_details",
                table: "ServiceUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "next_of_kin",
                table: "ServiceUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "relationship_to_person",
                table: "ServiceUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "skill_id",
                table: "AssessmentSkills",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("0b748e48-9852-4c8e-918e-bbc9d0ebbb8c"), null, null, new DateTimeOffset(new DateTime(2021, 9, 21, 5, 27, 30, 648, DateTimeKind.Unspecified).AddTicks(5518), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 0, null, null, 1 },
                    { new Guid("e1142b3b-0cc5-4157-8929-4c329dc1a9e9"), null, null, new DateTimeOffset(new DateTime(2021, 9, 21, 5, 27, 30, 648, DateTimeKind.Unspecified).AddTicks(6850), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 0, null, null, 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AssessmentSkills_LivingSkills_skill_id",
                table: "AssessmentSkills",
                column: "skill_id",
                principalTable: "LivingSkills",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
