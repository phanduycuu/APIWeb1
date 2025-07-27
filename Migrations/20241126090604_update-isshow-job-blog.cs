using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIWeb1.Migrations
{
    /// <inheritdoc />
    public partial class updateisshowjobblog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsShow",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShow",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "10d4c887-23e0-4994-9375-54a6642aef34", null, "User", "USER" },
                    { "1bdbc6bd-4c7c-4621-b0e5-39abba44b76a", null, "Employer", "EMPLOYER" },
                    { "ea92b0f5-fccf-40fc-8160-aec628d13518", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 26, 16, 6, 3, 514, DateTimeKind.Local).AddTicks(224), new DateTime(2024, 11, 26, 16, 6, 3, 514, DateTimeKind.Local).AddTicks(242) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 26, 16, 6, 3, 514, DateTimeKind.Local).AddTicks(245), new DateTime(2024, 11, 26, 16, 6, 3, 514, DateTimeKind.Local).AddTicks(245) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10d4c887-23e0-4994-9375-54a6642aef34");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bdbc6bd-4c7c-4621-b0e5-39abba44b76a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea92b0f5-fccf-40fc-8160-aec628d13518");

            migrationBuilder.DropColumn(
                name: "IsShow",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsShow",
                table: "Blogs");

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
    }
}
