using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class UserReferenceToDocUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("3928eeab-7e18-464e-ae09-812889d74b70"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("ef466c47-483c-4f7b-b917-10899823074e"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "DocumentUploads",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("709a111e-54a0-4f77-915b-ec6835c8cb09"), null, new DateTimeOffset(new DateTime(2021, 8, 18, 11, 51, 35, 42, DateTimeKind.Unspecified).AddTicks(7465), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, 1 },
                    { new Guid("8850b38f-0747-44c9-b5cb-aef5e0aeee31"), null, new DateTimeOffset(new DateTime(2021, 8, 18, 11, 51, 35, 42, DateTimeKind.Unspecified).AddTicks(8349), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentUploads_user_id",
                table: "DocumentUploads",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentUploads_Users_user_id",
                table: "DocumentUploads",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentUploads_Users_user_id",
                table: "DocumentUploads");

            migrationBuilder.DropIndex(
                name: "IX_DocumentUploads_user_id",
                table: "DocumentUploads");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("709a111e-54a0-4f77-915b-ec6835c8cb09"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("8850b38f-0747-44c9-b5cb-aef5e0aeee31"));

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "DocumentUploads");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("ef466c47-483c-4f7b-b917-10899823074e"), null, new DateTimeOffset(new DateTime(2021, 8, 16, 13, 33, 46, 500, DateTimeKind.Unspecified).AddTicks(3910), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, 1 },
                    { new Guid("3928eeab-7e18-464e-ae09-812889d74b70"), null, new DateTimeOffset(new DateTime(2021, 8, 16, 13, 33, 46, 500, DateTimeKind.Unspecified).AddTicks(4862), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, 1 }
                });
        }
    }
}
