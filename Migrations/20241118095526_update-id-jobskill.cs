using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIWeb1.Migrations
{
    /// <inheritdoc />
    public partial class updateidjobskill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Id",
                table: "JobSkills");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b2fddf2b-9be0-4178-bb69-184efd18acef", null, "User", "USER" },
                    { "d5b660f3-ef86-4a31-9eff-2599ca363c48", null, "Admin", "ADMIN" },
                    { "ea740e91-8ae0-4f19-84de-254f08cd378a", null, "Employer", "EMPLOYER" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 18, 16, 55, 25, 365, DateTimeKind.Local).AddTicks(7222), new DateTime(2024, 11, 18, 16, 55, 25, 365, DateTimeKind.Local).AddTicks(7234) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 18, 16, 55, 25, 365, DateTimeKind.Local).AddTicks(7236), new DateTime(2024, 11, 18, 16, 55, 25, 365, DateTimeKind.Local).AddTicks(7236) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2fddf2b-9be0-4178-bb69-184efd18acef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5b660f3-ef86-4a31-9eff-2599ca363c48");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea740e91-8ae0-4f19-84de-254f08cd378a");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "JobSkills",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
