using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIWeb1.Migrations
{
    /// <inheritdoc />
    public partial class updatestatusappuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43f4ed1a-013f-42a9-af03-8bde15b4f6bb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8373e7ae-8fe6-43cd-a48d-5cb8b7948c76");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ce987ad-4dec-4303-98fa-a2b8cbc5fc24");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Cv",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39cd1079-3d8b-43c5-9c41-a2118b0910a8", null, "Employer", "EMPLOYER" },
                    { "43b68d5d-a87e-45b2-8e39-6b59d5449a0d", null, "Admin", "ADMIN" },
                    { "5a5b70a9-3ff3-441d-806b-4f7cc5b14d51", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 11, 15, 43, 8, 454, DateTimeKind.Local).AddTicks(4547), new DateTime(2024, 11, 11, 15, 43, 8, 454, DateTimeKind.Local).AddTicks(4562) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 11, 15, 43, 8, 454, DateTimeKind.Local).AddTicks(4566), new DateTime(2024, 11, 11, 15, 43, 8, 454, DateTimeKind.Local).AddTicks(4568) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39cd1079-3d8b-43c5-9c41-a2118b0910a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43b68d5d-a87e-45b2-8e39-6b59d5449a0d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a5b70a9-3ff3-441d-806b-4f7cc5b14d51");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Cv",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "43f4ed1a-013f-42a9-af03-8bde15b4f6bb", null, "User", "USER" },
                    { "8373e7ae-8fe6-43cd-a48d-5cb8b7948c76", null, "Employer", "EMPLOYER" },
                    { "8ce987ad-4dec-4303-98fa-a2b8cbc5fc24", null, "Admin", "ADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 8, 21, 34, 29, 712, DateTimeKind.Local).AddTicks(5925), new DateTime(2024, 11, 8, 21, 34, 29, 712, DateTimeKind.Local).AddTicks(5946) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 8, 21, 34, 29, 712, DateTimeKind.Local).AddTicks(5993), new DateTime(2024, 11, 8, 21, 34, 29, 712, DateTimeKind.Local).AddTicks(5994) });
        }
    }
}
