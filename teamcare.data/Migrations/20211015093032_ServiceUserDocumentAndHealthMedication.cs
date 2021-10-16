using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
    public partial class ServiceUserDocumentAndHealthMedication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "id",
            //    keyValue: new Guid("a258c184-c0f0-4f3b-ba8d-87bf934ca593"));

            //migrationBuilder.DeleteData(
            //    table: "Users",
            //    keyColumn: "id",
            //    keyValue: new Guid("ebc90c82-ba40-453b-a4de-324089df2b22"));

            migrationBuilder.AddColumn<Guid>(
                name: "service_user_document_id",
                table: "DocumentUploads",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HealthMedications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    diagnosis = table.Column<string>(type: "text", nullable: true),
                    prescription_history = table.Column<string>(type: "text", nullable: true),
                    social_history = table.Column<string>(type: "text", nullable: true),
                    allergy_information = table.Column<string>(type: "text", nullable: true),
                    other_significant_information = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    last_updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthMedications", x => x.id);
                    table.ForeignKey(
                        name: "FK_HealthMedications_ServiceUsers_service_user_id",
                        column: x => x.service_user_id,
                        principalTable: "ServiceUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthMedications_Users_created_by",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceUserDocuments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_received = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    document_category = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    last_updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    last_updated_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_on = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceUserDocuments", x => x.id);
                    table.ForeignKey(
                        name: "FK_ServiceUserDocuments_ServiceUsers_service_user_id",
                        column: x => x.service_user_id,
                        principalTable: "ServiceUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceUserDocuments_Users_created_by",
                        column: x => x.created_by,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            //migrationBuilder.InsertData(
            //    table: "Users",
            //    columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
            //    values: new object[,]
            //    {
            //        { new Guid("3708c8fd-e14a-4684-b15e-0002d1289f36"), null, null, new DateTimeOffset(new DateTime(2021, 10, 15, 9, 30, 31, 934, DateTimeKind.Unspecified).AddTicks(6675), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 1, null, null, 1 },
            //        { new Guid("f6f064bd-a28b-4d62-97ab-78e50f1f0e3f"), null, null, new DateTimeOffset(new DateTime(2021, 10, 15, 9, 30, 31, 934, DateTimeKind.Unspecified).AddTicks(8116), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 4, null, null, 1 }
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentUploads_service_user_document_id",
                table: "DocumentUploads",
                column: "service_user_document_id");

            migrationBuilder.CreateIndex(
                name: "IX_HealthMedications_created_by",
                table: "HealthMedications",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_HealthMedications_service_user_id",
                table: "HealthMedications",
                column: "service_user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUserDocuments_created_by",
                table: "ServiceUserDocuments",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceUserDocuments_service_user_id",
                table: "ServiceUserDocuments",
                column: "service_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentUploads_ServiceUserDocuments_service_user_document_~",
                table: "DocumentUploads",
                column: "service_user_document_id",
                principalTable: "ServiceUserDocuments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentUploads_ServiceUserDocuments_service_user_document_~",
                table: "DocumentUploads");

            migrationBuilder.DropTable(
                name: "HealthMedications");

            migrationBuilder.DropTable(
                name: "ServiceUserDocuments");

            migrationBuilder.DropIndex(
                name: "IX_DocumentUploads_service_user_document_id",
                table: "DocumentUploads");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("3708c8fd-e14a-4684-b15e-0002d1289f36"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "id",
                keyValue: new Guid("f6f064bd-a28b-4d62-97ab-78e50f1f0e3f"));

            migrationBuilder.DropColumn(
                name: "service_user_document_id",
                table: "DocumentUploads");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
                values: new object[,]
                {
                    { new Guid("ebc90c82-ba40-453b-a4de-324089df2b22"), null, null, new DateTimeOffset(new DateTime(2021, 10, 8, 10, 3, 21, 547, DateTimeKind.Unspecified).AddTicks(2400), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 0, null, null, 1 },
                    { new Guid("a258c184-c0f0-4f3b-ba8d-87bf934ca593"), null, null, new DateTimeOffset(new DateTime(2021, 10, 8, 10, 3, 21, 547, DateTimeKind.Unspecified).AddTicks(4751), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 0, null, null, 1 }
                });
        }
    }
}
