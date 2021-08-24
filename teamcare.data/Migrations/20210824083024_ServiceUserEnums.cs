using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teamcare.data.Migrations
{
	public partial class ServiceUserEnums : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "Users",
				keyColumn: "id",
				keyValue: new Guid("3dd61635-81ad-4f82-9c26-61b8362be37e"));

			migrationBuilder.DeleteData(
				table: "Users",
				keyColumn: "id",
				keyValue: new Guid("b6b3c284-f898-4fc0-8424-1d28e1e174dc"));

			migrationBuilder.DropColumn(
				name: "title",
				table: "ServiceUsers");

			migrationBuilder.AddColumn<int>(
				name: "title",
				table: "ServiceUsers",
				type: "integer",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.DropColumn(
				name: "religion",
				table: "ServiceUsers");

			migrationBuilder.AddColumn<int>(
				name: "religion",
				table: "ServiceUsers",
				type: "integer",
				nullable: false,
				defaultValue: 0);




			migrationBuilder.DropColumn(
				name: "preferred_first_language",
				table: "ServiceUsers");

			migrationBuilder.AddColumn<int>(
				name: "preferred_first_language",
				table: "ServiceUsers",
				type: "integer",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.DropColumn(
				name: "marital_status",
				table: "ServiceUsers");

			migrationBuilder.AddColumn<int>(
				name: "marital_status",
				table: "ServiceUsers",
				type: "integer",
				nullable: false,
				defaultValue: 0);


			migrationBuilder.DropColumn(
				name: "ethnicity",
				table: "ServiceUsers");

			migrationBuilder.AddColumn<int>(
				name: "ethnicity",
				table: "ServiceUsers",
				type: "integer",
				nullable: false,
				defaultValue: 0);



			migrationBuilder.InsertData(
				table: "Users",
				columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
				values: new object[,]
				{
					{ new Guid("2aea7ff6-f70c-4851-9272-5af20948d4d5"), null, null, new DateTimeOffset(new DateTime(2021, 8, 24, 8, 30, 23, 629, DateTimeKind.Unspecified).AddTicks(6307), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, null, null, 1 },
					{ new Guid("df157b62-b838-4af4-805c-87b5ac6e7033"), null, null, new DateTimeOffset(new DateTime(2021, 8, 24, 8, 30, 23, 629, DateTimeKind.Unspecified).AddTicks(7609), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, null, null, 1 }
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "Users",
				keyColumn: "id",
				keyValue: new Guid("2aea7ff6-f70c-4851-9272-5af20948d4d5"));

			migrationBuilder.DeleteData(
				table: "Users",
				keyColumn: "id",
				keyValue: new Guid("df157b62-b838-4af4-805c-87b5ac6e7033"));

			migrationBuilder.AlterColumn<string>(
				name: "title",
				table: "ServiceUsers",
				type: "text",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "integer");

			migrationBuilder.AlterColumn<string>(
				name: "religion",
				table: "ServiceUsers",
				type: "text",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "integer");

			migrationBuilder.AlterColumn<string>(
				name: "preferred_first_language",
				table: "ServiceUsers",
				type: "text",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "integer");

			migrationBuilder.AlterColumn<string>(
				name: "marital_status",
				table: "ServiceUsers",
				type: "text",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "integer");

			migrationBuilder.AlterColumn<string>(
				name: "ethnicity",
				table: "ServiceUsers",
				type: "text",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "integer");

			migrationBuilder.InsertData(
				table: "Users",
				columns: new[] { "id", "address", "created_by", "created_on", "deleted_by", "deleted_on", "email", "first_name", "is_active", "last_name", "mobile_no", "phone_no", "position", "title", "last_updated_by", "last_updated_on", "user_role" },
				values: new object[,]
				{
					{ new Guid("b6b3c284-f898-4fc0-8424-1d28e1e174dc"), null, null, new DateTimeOffset(new DateTime(2021, 8, 23, 5, 44, 0, 999, DateTimeKind.Unspecified).AddTicks(4183), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hans.doyekee@gmail.com", "Teamcare", true, "Admin", null, null, null, null, null, null, 1 },
					{ new Guid("3dd61635-81ad-4f82-9c26-61b8362be37e"), null, null, new DateTimeOffset(new DateTime(2021, 8, 23, 5, 44, 0, 999, DateTimeKind.Unspecified).AddTicks(5447), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nish.kagathara0791@gmail.com", "Nishidh", true, "Kagathara", null, null, null, null, null, null, 1 }
				});
		}
	}
}
