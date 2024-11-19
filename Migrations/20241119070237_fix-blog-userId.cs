using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIWeb1.Migrations
{
    /// <inheritdoc />
    public partial class fixbloguserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_EmployerId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_EmployerId",
                table: "Blogs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11bb721f-67e7-41b3-a8e4-cd6ae005edc6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ba415fb-bade-4464-9d66-2fcbed8fb7d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "353fe11b-5b42-4747-bb49-c65a9df91068");

            migrationBuilder.DropColumn(
                name: "EmployerId",
                table: "Blogs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Blogs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_UserId",
                table: "Blogs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_UserId",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs");

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

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "EmployerId",
                table: "Blogs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "11bb721f-67e7-41b3-a8e4-cd6ae005edc6", null, "Admin", "ADMIN" },
                    { "2ba415fb-bade-4464-9d66-2fcbed8fb7d8", null, "User", "USER" },
                    { "353fe11b-5b42-4747-bb49-c65a9df91068", null, "Employer", "EMPLOYER" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 19, 13, 33, 55, 64, DateTimeKind.Local).AddTicks(7414), new DateTime(2024, 11, 19, 13, 33, 55, 64, DateTimeKind.Local).AddTicks(7428) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 19, 13, 33, 55, 64, DateTimeKind.Local).AddTicks(7431), new DateTime(2024, 11, 19, 13, 33, 55, 64, DateTimeKind.Local).AddTicks(7431) });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_EmployerId",
                table: "Blogs",
                column: "EmployerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_EmployerId",
                table: "Blogs",
                column: "EmployerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
