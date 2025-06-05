using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RolesAdded3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "3c8f59e7-56de-4bc5-bf71-2e29b1a769f9", null, "Customer", "CUSTOMER" },
                    { "d2d8f7e1-f63c-46f1-b8b4-43c5f3c2f3e1", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c8f59e7-56de-4bc5-bf71-2e29b1a769f9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2d8f7e1-f63c-46f1-b8b4-43c5f3c2f3e1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d54f9191-1bc3-42ec-a6d5-2bf5e73c8f3f", null, "Customer", "CUSTOMER" },
                    { "d7c5b78a-f49e-4954-973e-4f0312e744ce", null, "Admin", "ADMIN" }
                });
        }
    }
}
