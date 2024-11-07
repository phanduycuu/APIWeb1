using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIWeb1.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ba05325-66a7-480b-ad80-ebe883f06475");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "125da457-77fc-4f5b-86a3-431d728f5403");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7764146-7e6d-4a47-b82e-767e26fdc629");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3d54364d-b558-4fcc-8eb4-8fe4c4b8c1c2", null, "Employer", "EMPLOYER" },
                    { "4666d5a6-7a8a-412c-93b0-0bd300635ae1", null, "Admin", "ADMIN" },
                    { "83dc3b87-d2e4-4d8c-9389-dae710cc3a7f", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "Create", "Description", "Email", "Industry", "Location", "Logo", "Name", "Phone", "Status", "Update", "Website" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 7, 19, 42, 14, 314, DateTimeKind.Local).AddTicks(5565), "Công ti về công nghệ hàng đầu thế giới", "FPT@gmail.com", "Information technology", "HCM", "/abc.png", "FPT", "0368166471", true, new DateTime(2024, 11, 7, 19, 42, 14, 314, DateTimeKind.Local).AddTicks(5576), "FPT.com" },
                    { 2, new DateTime(2024, 11, 7, 19, 42, 14, 314, DateTimeKind.Local).AddTicks(5578), "Đa lĩnh vực", "BOSCH@gmail.com", "Information technology", "HCM", "/xyz.png", "BOSCH", "0368166471", true, new DateTime(2024, 11, 7, 19, 42, 14, 314, DateTimeKind.Local).AddTicks(5579), "BOSCH.com" }
                });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 8,
                column: "JobId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 9,
                column: "JobId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 10,
                column: "JobId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_JobId",
                table: "Skills",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Jobs_JobId",
                table: "Skills",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Jobs_JobId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_JobId",
                table: "Skills");

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

            migrationBuilder.DeleteData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Skills");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ba05325-66a7-480b-ad80-ebe883f06475", null, "User", "USER" },
                    { "125da457-77fc-4f5b-86a3-431d728f5403", null, "Employer", "EMPLOYER" },
                    { "d7764146-7e6d-4a47-b82e-767e26fdc629", null, "Admin", "ADMIN" }
                });
        }
    }
}
