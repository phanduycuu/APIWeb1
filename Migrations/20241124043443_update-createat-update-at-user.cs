using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIWeb1.Migrations
{
    /// <inheritdoc />
    public partial class updatecreateatupdateatuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "559245ba-6641-4966-97e3-7b05db39e17d", null, "Employer", "EMPLOYER" },
                    { "60ad35e2-a5e7-4bf5-930e-54cf22cae6f0", null, "User", "USER" },
                    { "72c27f40-13bc-4193-834b-493ffb8d3364", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 24, 11, 34, 41, 983, DateTimeKind.Local).AddTicks(6999), new DateTime(2024, 11, 24, 11, 34, 41, 983, DateTimeKind.Local).AddTicks(7010) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 24, 11, 34, 41, 983, DateTimeKind.Local).AddTicks(7012), new DateTime(2024, 11, 24, 11, 34, 41, 983, DateTimeKind.Local).AddTicks(7013) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "559245ba-6641-4966-97e3-7b05db39e17d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "60ad35e2-a5e7-4bf5-930e-54cf22cae6f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72c27f40-13bc-4193-834b-493ffb8d3364");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "AspNetUsers");

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
    }
}
