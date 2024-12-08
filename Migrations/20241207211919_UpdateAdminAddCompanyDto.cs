using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIWeb1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdminAddCompanyDto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0dc2d014-f00a-4403-aeb5-83bf1c3a8c78", null, "Admin", "ADMIN" },
                    { "72f59648-cfea-453d-b15b-ba7c03ec33db", null, "Employer", "EMPLOYER" },
                    { "9fafdccd-f892-4492-b3f2-105cb92d8da5", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 12, 8, 4, 19, 18, 922, DateTimeKind.Local).AddTicks(5160), new DateTime(2024, 12, 8, 4, 19, 18, 922, DateTimeKind.Local).AddTicks(5178) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 12, 8, 4, 19, 18, 922, DateTimeKind.Local).AddTicks(5181), new DateTime(2024, 12, 8, 4, 19, 18, 922, DateTimeKind.Local).AddTicks(5182) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0dc2d014-f00a-4403-aeb5-83bf1c3a8c78");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72f59648-cfea-453d-b15b-ba7c03ec33db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9fafdccd-f892-4492-b3f2-105cb92d8da5");

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
    }
}
