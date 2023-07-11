using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzBook.Migrations
{
    public partial class coverFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: "73f52f10-9d18-4c24-83b0-a4004d20ab19");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: "788d7965-5110-4753-99da-ff9a4271520c");

            migrationBuilder.AddColumn<string>(
                name: "BookCoverUrl",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "BookCoverUrl", "CreatedAt", "CreatedBy", "Description", "Quantity", "Status", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[] { "21466801-9deb-4fb0-98e5-ff5536ce1609", "Author 1", null, new DateTime(2023, 7, 7, 15, 56, 12, 764, DateTimeKind.Local).AddTicks(7407), "Seeder", "Description 1", 1, null, "Book 1", null, null });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "BookCoverUrl", "CreatedAt", "CreatedBy", "Description", "Quantity", "Status", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[] { "47650f11-b4ed-4eda-9c51-e102d42976dc", "Author 2", null, new DateTime(2023, 7, 7, 15, 56, 12, 764, DateTimeKind.Local).AddTicks(7411), "Seeder", "Description 2", 1, null, "Book 2", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: "21466801-9deb-4fb0-98e5-ff5536ce1609");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: "47650f11-b4ed-4eda-9c51-e102d42976dc");

            migrationBuilder.DropColumn(
                name: "BookCoverUrl",
                table: "Books");

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CreatedAt", "CreatedBy", "Description", "Quantity", "Status", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[] { "73f52f10-9d18-4c24-83b0-a4004d20ab19", "Author 1", new DateTime(2023, 6, 22, 19, 2, 12, 114, DateTimeKind.Local).AddTicks(3636), "Seeder", "Description 1", 1, null, "Book 1", null, null });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CreatedAt", "CreatedBy", "Description", "Quantity", "Status", "Title", "UpdatedAt", "UpdatedBy" },
                values: new object[] { "788d7965-5110-4753-99da-ff9a4271520c", "Author 2", new DateTime(2023, 6, 22, 19, 2, 12, 114, DateTimeKind.Local).AddTicks(3641), "Seeder", "Description 2", 1, null, "Book 2", null, null });
        }
    }
}
