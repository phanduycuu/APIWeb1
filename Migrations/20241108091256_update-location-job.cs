using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIWeb1.Migrations
{
    /// <inheritdoc />
    public partial class updatelocationjob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "795b7d7e-0978-45d0-9e2a-bc3861ebcc8f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1a21713-d2a0-4045-93d8-5301a41b8ac3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f20400f0-4fb4-4d48-bd21-ee2da4e0baa3");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "28d65ec2-da93-4d8a-a689-d3f0954a1646", null, "Employer", "EMPLOYER" },
                    { "58e90dff-77a2-4ffb-9390-447581048825", null, "Admin", "ADMIN" },
                    { "fd807f66-e6c0-4724-8a18-5fdcbe877df0", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 12, 55, 97, DateTimeKind.Local).AddTicks(2166), new DateTime(2024, 11, 8, 16, 12, 55, 97, DateTimeKind.Local).AddTicks(2182) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 8, 16, 12, 55, 97, DateTimeKind.Local).AddTicks(2184), new DateTime(2024, 11, 8, 16, 12, 55, 97, DateTimeKind.Local).AddTicks(2185) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28d65ec2-da93-4d8a-a689-d3f0954a1646");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58e90dff-77a2-4ffb-9390-447581048825");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd807f66-e6c0-4724-8a18-5fdcbe877df0");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Jobs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "795b7d7e-0978-45d0-9e2a-bc3861ebcc8f", null, "Employer", "EMPLOYER" },
                    { "f1a21713-d2a0-4045-93d8-5301a41b8ac3", null, "User", "USER" },
                    { "f20400f0-4fb4-4d48-bd21-ee2da4e0baa3", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 7, 21, 26, 47, 742, DateTimeKind.Local).AddTicks(1620), new DateTime(2024, 11, 7, 21, 26, 47, 742, DateTimeKind.Local).AddTicks(1636) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 7, 21, 26, 47, 742, DateTimeKind.Local).AddTicks(1638), new DateTime(2024, 11, 7, 21, 26, 47, 742, DateTimeKind.Local).AddTicks(1639) });
        }
    }
}
