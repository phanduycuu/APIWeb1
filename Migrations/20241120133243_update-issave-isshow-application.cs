using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIWeb1.Migrations
{
    /// <inheritdoc />
    public partial class updateissaveisshowapplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d1bd9d5-5aea-4784-9124-b4a4cb77e360");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "779a7e8e-7d2e-4b12-8b2e-f8a3adf2e0f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c86c6f0a-b680-4394-a69a-e199356a6e31");

            migrationBuilder.RenameColumn(
                name: "IsSale",
                table: "Applications",
                newName: "Isshow");

            migrationBuilder.AddColumn<bool>(
                name: "Issave",
                table: "Applications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38f9f251-2c84-4824-bb66-8f3cd743c251", null, "Employer", "EMPLOYER" },
                    { "66b6ae2b-b04a-4b4c-b407-c41aee97f912", null, "User", "USER" },
                    { "bb3ef61f-136a-4bea-a42f-1be3386848ed", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 20, 20, 32, 41, 997, DateTimeKind.Local).AddTicks(8163), new DateTime(2024, 11, 20, 20, 32, 41, 997, DateTimeKind.Local).AddTicks(8177) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 20, 20, 32, 41, 997, DateTimeKind.Local).AddTicks(8179), new DateTime(2024, 11, 20, 20, 32, 41, 997, DateTimeKind.Local).AddTicks(8180) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38f9f251-2c84-4824-bb66-8f3cd743c251");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66b6ae2b-b04a-4b4c-b407-c41aee97f912");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb3ef61f-136a-4bea-a42f-1be3386848ed");

            migrationBuilder.DropColumn(
                name: "Issave",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "Isshow",
                table: "Applications",
                newName: "IsSale");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2d1bd9d5-5aea-4784-9124-b4a4cb77e360", null, "User", "USER" },
                    { "779a7e8e-7d2e-4b12-8b2e-f8a3adf2e0f6", null, "Employer", "EMPLOYER" },
                    { "c86c6f0a-b680-4394-a69a-e199356a6e31", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 19, 14, 2, 37, 274, DateTimeKind.Local).AddTicks(4347), new DateTime(2024, 11, 19, 14, 2, 37, 274, DateTimeKind.Local).AddTicks(4360) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 19, 14, 2, 37, 274, DateTimeKind.Local).AddTicks(4363), new DateTime(2024, 11, 19, 14, 2, 37, 274, DateTimeKind.Local).AddTicks(4363) });
        }
    }
}
