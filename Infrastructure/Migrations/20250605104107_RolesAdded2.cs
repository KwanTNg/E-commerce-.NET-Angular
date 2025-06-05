using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RolesAdded2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d0234ca-14b8-4f6a-9298-1ef5dd41c84f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd881cc0-a658-4a85-b178-73b69c29245d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d54f9191-1bc3-42ec-a6d5-2bf5e73c8f3f", null, "Customer", "CUSTOMER" },
                    { "d7c5b78a-f49e-4954-973e-4f0312e744ce", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d54f9191-1bc3-42ec-a6d5-2bf5e73c8f3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7c5b78a-f49e-4954-973e-4f0312e744ce");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5d0234ca-14b8-4f6a-9298-1ef5dd41c84f", null, "Admin", "ADMIN" },
                    { "dd881cc0-a658-4a85-b178-73b69c29245d", null, "Customer", "CUSTOMER" }
                });
        }
    }
}
