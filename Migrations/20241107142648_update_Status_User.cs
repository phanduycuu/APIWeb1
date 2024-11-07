using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIWeb1.Migrations
{
    /// <inheritdoc />
    public partial class update_Status_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d54364d-b558-4fcc-8eb4-8fe4c4b8c1c2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4666d5a6-7a8a-412c-93b0-0bd300635ae1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "83dc3b87-d2e4-4d8c-9389-dae710cc3a7f");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Status",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3d54364d-b558-4fcc-8eb4-8fe4c4b8c1c2", null, "Employer", "EMPLOYER" },
                    { "4666d5a6-7a8a-412c-93b0-0bd300635ae1", null, "Admin", "ADMIN" },
                    { "83dc3b87-d2e4-4d8c-9389-dae710cc3a7f", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 7, 19, 42, 14, 314, DateTimeKind.Local).AddTicks(5565), new DateTime(2024, 11, 7, 19, 42, 14, 314, DateTimeKind.Local).AddTicks(5576) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 7, 19, 42, 14, 314, DateTimeKind.Local).AddTicks(5578), new DateTime(2024, 11, 7, 19, 42, 14, 314, DateTimeKind.Local).AddTicks(5579) });
        }
    }
}
