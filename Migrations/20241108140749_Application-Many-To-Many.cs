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
                    JobId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateApply = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsSale = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => new { x.JobId, x.UserId });
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "02b42119-76e3-4e9c-9f08-534f5aa38d63", null, "Admin", "ADMIN" },
                    { "858744de-f1bd-48dd-8f84-75ea96c46b0b", null, "Employer", "EMPLOYER" },
                    { "dae2c740-d75e-4826-8fe0-0e64a41e3037", null, "User", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 8, 21, 7, 49, 63, DateTimeKind.Local).AddTicks(2827), new DateTime(2024, 11, 8, 21, 7, 49, 63, DateTimeKind.Local).AddTicks(2839) });

            migrationBuilder.UpdateData(
                table: "Companys",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Create", "Update" },
                values: new object[] { new DateTime(2024, 11, 8, 21, 7, 49, 63, DateTimeKind.Local).AddTicks(2842), new DateTime(2024, 11, 8, 21, 7, 49, 63, DateTimeKind.Local).AddTicks(2842) });

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
                keyValue: "02b42119-76e3-4e9c-9f08-534f5aa38d63");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "858744de-f1bd-48dd-8f84-75ea96c46b0b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dae2c740-d75e-4826-8fe0-0e64a41e3037");

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
