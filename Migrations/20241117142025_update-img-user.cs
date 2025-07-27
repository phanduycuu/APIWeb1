using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIWeb1.Migrations
{
    /// <inheritdoc />
    public partial class updateimguser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1697d92a-8aa2-42cd-9f53-a018238aac4f", null, "Admin", "ADMIN" },
                    { "ecf7c4b9-5e01-4c76-8c2b-833ab6c7dcf0", null, "Employer", "EMPLOYER" },
                    { "f9d42e42-3065-4766-956c-541120dbfc00", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 17, 21, 20, 24, 679, DateTimeKind.Local).AddTicks(1107), new DateTime(2024, 11, 17, 21, 20, 24, 679, DateTimeKind.Local).AddTicks(1120) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 17, 21, 20, 24, 679, DateTimeKind.Local).AddTicks(1122), new DateTime(2024, 11, 17, 21, 20, 24, 679, DateTimeKind.Local).AddTicks(1123) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1697d92a-8aa2-42cd-9f53-a018238aac4f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ecf7c4b9-5e01-4c76-8c2b-833ab6c7dcf0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9d42e42-3065-4766-956c-541120dbfc00");

            migrationBuilder.DropColumn(
                name: "Img",
                table: "AspNetUsers");

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
    }
}
