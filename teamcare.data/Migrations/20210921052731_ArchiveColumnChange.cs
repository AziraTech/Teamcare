using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
	public partial class ArchiveColumnChange : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "Users",
				keyColumn: "id",
				keyValue: new Guid("124a8175-388c-45ce-853d-a6fabe25831b"));

			migrationBuilder.DeleteData(
				table: "Users",
				keyColumn: "id",
				keyValue: new Guid("3ab28e10-ae92-4a2c-84af-2155be61a74d"));

			migrationBuilder.DropColumn(
		  name: "archived_reason",
		  table: "ServiceUsers");

			migrationBuilder.AddColumn<int>(
				name: "archived_reason",
				table: "ServiceUsers",
				nullable: false,
				type: "integer",
				defaultValue: 0);

			migrationBuilder.InsertData(
				table: "Users",
				columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
				values: new object[,]
				{
					{ new Guid("0b748e48-9852-4c8e-918e-bbc9d0ebbb8c"), null, null, new DateTimeOffset(new DateTime(2021, 9, 21, 5, 27, 30, 648, DateTimeKind.Unspecified).AddTicks(5518), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 0, null, null, 1 },
					{ new Guid("e1142b3b-0cc5-4157-8929-4c329dc1a9e9"), null, null, new DateTimeOffset(new DateTime(2021, 9, 21, 5, 27, 30, 648, DateTimeKind.Unspecified).AddTicks(6850), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 0, null, null, 1 }
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "Users",
				keyColumn: "id",
				keyValue: new Guid("0b748e48-9852-4c8e-918e-bbc9d0ebbb8c"));

			migrationBuilder.DeleteData(
				table: "Users",
				keyColumn: "id",
				keyValue: new Guid("e1142b3b-0cc5-4157-8929-4c329dc1a9e9"));



			migrationBuilder.DropColumn(
			   name: "archived_reason",
			   table: "ServiceUsers");

			migrationBuilder.AddColumn<string>(
				name: "archived_reason",
				table: "ServiceUsers",
				nullable: true,
				type: "text");

			migrationBuilder.InsertData(
				table: "Users",
				columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
				values: new object[,]
				{
					{ new Guid("124a8175-388c-45ce-853d-a6fabe25831b"), null, null, new DateTimeOffset(new DateTime(2021, 9, 17, 9, 16, 47, 542, DateTimeKind.Unspecified).AddTicks(9151), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, 0, null, null, 1 },
					{ new Guid("3ab28e10-ae92-4a2c-84af-2155be61a74d"), null, null, new DateTimeOffset(new DateTime(2021, 9, 17, 9, 16, 47, 543, DateTimeKind.Unspecified).AddTicks(1653), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, 0, null, null, 1 }
				});
		}
	}
}
