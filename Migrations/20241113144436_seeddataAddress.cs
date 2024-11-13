using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIWeb1.Migrations
{
    /// <inheritdoc />
    public partial class seeddataAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6da15bad-8ba7-448e-8911-a9f5cdadc99a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b6fd433-a5bf-4149-9fc7-0ed2b3bedc82");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eab21be7-1b1b-476a-9a08-86ac8ca48852");

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "District", "Province", "Street", "Ward" },
                values: new object[] { 1, "Phú thọ hòa", "HCM ", "Lê thúc hoạch", "Tân phú" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b40d68c5-476b-42e4-857c-a20a288aaec4", null, "Employer", "EMPLOYER" },
                    { "cc33bc40-2eba-489c-9c55-458f798d9ae6", null, "Admin", "ADMIN" },
                    { "f9e0aaef-4918-4bb5-b44e-8f15f461a88a", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 13, 21, 44, 35, 778, DateTimeKind.Local).AddTicks(8428), new DateTime(2024, 11, 13, 21, 44, 35, 778, DateTimeKind.Local).AddTicks(8443) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 13, 21, 44, 35, 778, DateTimeKind.Local).AddTicks(8445), new DateTime(2024, 11, 13, 21, 44, 35, 778, DateTimeKind.Local).AddTicks(8446) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b40d68c5-476b-42e4-857c-a20a288aaec4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc33bc40-2eba-489c-9c55-458f798d9ae6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9e0aaef-4918-4bb5-b44e-8f15f461a88a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6da15bad-8ba7-448e-8911-a9f5cdadc99a", null, "User", "USER" },
                    { "8b6fd433-a5bf-4149-9fc7-0ed2b3bedc82", null, "Admin", "ADMIN" },
                    { "eab21be7-1b1b-476a-9a08-86ac8ca48852", null, "Employer", "EMPLOYER" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 13, 20, 40, 46, 932, DateTimeKind.Local).AddTicks(666), new DateTime(2024, 11, 13, 20, 40, 46, 932, DateTimeKind.Local).AddTicks(684) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 13, 20, 40, 46, 932, DateTimeKind.Local).AddTicks(687), new DateTime(2024, 11, 13, 20, 40, 46, 932, DateTimeKind.Local).AddTicks(687) });
        }
    }
}
