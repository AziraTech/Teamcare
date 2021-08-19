using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class AddUserToDocumentUploads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("23312baf-9613-4c89-a84b-abce8cf454a6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("63245849-c8ea-4484-b954-82a1e688a0e8"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "DocumentUploads",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("26fe31e7-53ac-4f06-8b51-2313d867f70e"), null, null, new DateTimeOffset(new DateTime(2021, 8, 18, 21, 28, 56, 385, DateTimeKind.Unspecified).AddTicks(1911), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, null, null, null, null, 1 },
                    { new Guid("cf0f483d-aebf-4cb8-baaa-98810e9f1317"), null, null, new DateTimeOffset(new DateTime(2021, 8, 18, 21, 28, 56, 385, DateTimeKind.Unspecified).AddTicks(4287), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, null, null, null, null, 1 }
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
                keyValue: new Guid("26fe31e7-53ac-4f06-8b51-2313d867f70e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("cf0f483d-aebf-4cb8-baaa-98810e9f1317"));

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "DocumentUploads");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("63245849-c8ea-4484-b954-82a1e688a0e8"), null, null, new DateTimeOffset(new DateTime(2021, 8, 18, 8, 5, 25, 862, DateTimeKind.Unspecified).AddTicks(3447), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", false, "Admin", null, null, null, null, null, null, 1 },
                    { new Guid("23312baf-9613-4c89-a84b-abce8cf454a6"), null, null, new DateTimeOffset(new DateTime(2021, 8, 18, 8, 5, 25, 862, DateTimeKind.Unspecified).AddTicks(8924), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", false, "Kagathara", null, null, null, null, null, null, 1 }
                });
        }
    }
}
