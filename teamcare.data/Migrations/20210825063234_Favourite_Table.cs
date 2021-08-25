using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class Favourite_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("7101b354-5426-4931-89b9-5537c35e7102"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("be103524-bfec-41d4-83a5-a643e7c71ff5"));

            migrationBuilder.CreateTable(
                name: "FavouriteServiceUsers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    service_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    last_updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouriteServiceUsers", x => x.id);
                    table.ForeignKey(
                        name: "FK_FavouriteServiceUsers_ServiceUsers_service_user_id",
                        column: x => x.service_user_id,
                        principalTable: "ServiceUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavouriteServiceUsers_Users_created_by",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavouriteServiceUsers_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("e95c64b3-fc98-4942-a6a1-7e7448487e83"), null, null, new DateTimeOffset(new DateTime(2021, 8, 25, 6, 32, 34, 71, DateTimeKind.Unspecified).AddTicks(6239), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, null, null, 1 },
                    { new Guid("9878048d-ff7d-47e6-bec5-9d5fb4ade7e5"), null, null, new DateTimeOffset(new DateTime(2021, 8, 25, 6, 32, 34, 71, DateTimeKind.Unspecified).AddTicks(7349), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, null, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteServiceUsers_created_by",
                table: "FavouriteServiceUsers",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteServiceUsers_service_user_id",
                table: "FavouriteServiceUsers",
                column: "service_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteServiceUsers_user_id",
                table: "FavouriteServiceUsers",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouriteServiceUsers");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("9878048d-ff7d-47e6-bec5-9d5fb4ade7e5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("e95c64b3-fc98-4942-a6a1-7e7448487e83"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("be103524-bfec-41d4-83a5-a643e7c71ff5"), null, null, new DateTimeOffset(new DateTime(2021, 8, 25, 5, 42, 9, 249, DateTimeKind.Unspecified).AddTicks(473), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, null, null, 1 },
                    { new Guid("7101b354-5426-4931-89b9-5537c35e7102"), null, null, new DateTimeOffset(new DateTime(2021, 8, 25, 5, 42, 9, 249, DateTimeKind.Unspecified).AddTicks(1815), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, null, null, 1 }
                });
        }
    }
}
