using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIWeb1.Migrations
{
    /// <inheritdoc />
    public partial class addaddressupdatejobappusercompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Companys");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsFetured",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthdate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ea8995d-8458-4bc3-8e93-15316fbaa609", null, "Employer", "EMPLOYER" },
                    { "3b787bc1-7566-471f-8eb8-8cfd0925b191", null, "Admin", "ADMIN" },
                    { "c7a62a44-74cc-44ff-88d2-82730df6b772", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 13, 20, 36, 11, 601, DateTimeKind.Local).AddTicks(3316), new DateTime(2024, 11, 13, 20, 36, 11, 601, DateTimeKind.Local).AddTicks(3328) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 13, 20, 36, 11, 601, DateTimeKind.Local).AddTicks(3330), new DateTime(2024, 11, 13, 20, 36, 11, 601, DateTimeKind.Local).AddTicks(3331) });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_AddressId",
                table: "Jobs",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Address_AddressId",
                table: "AspNetUsers",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Address_AddressId",
                table: "Jobs",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Address_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Address_AddressId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_AddressId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AddressId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ea8995d-8458-4bc3-8e93-15316fbaa609");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b787bc1-7566-471f-8eb8-8cfd0925b191");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c7a62a44-74cc-44ff-88d2-82730df6b772");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsFetured",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Companys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                columns: new[] { "Create", "Location", "Update" },
                values: new object[] { new DateTime(2024, 11, 11, 15, 43, 8, 454, DateTimeKind.Local).AddTicks(4547), "HCM", new DateTime(2024, 11, 11, 15, 43, 8, 454, DateTimeKind.Local).AddTicks(4562) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Location", "Update" },
                values: new object[] { new DateTime(2024, 11, 11, 15, 43, 8, 454, DateTimeKind.Local).AddTicks(4566), "HCM", new DateTime(2024, 11, 11, 15, 43, 8, 454, DateTimeKind.Local).AddTicks(4568) });
        }
    }
}
