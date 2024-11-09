using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIWeb1.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateApply = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsSale = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobId",
                table: "Applications",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_UserId",
                table: "Applications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

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
    }
}
