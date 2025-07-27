using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIWeb1.Migrations
{
    /// <inheritdoc />
    public partial class addblog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_AspNetUsers_EmployerId",
                        column: x => x.EmployerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

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
    }
}
