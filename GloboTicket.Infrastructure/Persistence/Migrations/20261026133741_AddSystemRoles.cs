using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GloboTicket.Infrastructure.persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "382601e0 - 8d06 - 4bc6 - 9a04 - c9d5b169aa89", "162705e6-7360-4281-a427-e7137ae9ef64", "TenantAdmin", "CR-TENANTADMIN" },
                    { "577fab6a - 4196 - 4b7a - b8dd - 6a8a08a78a1c", "e8d758fd-3c82-4bdd-add6-828b31fec748", "CustomerUser", "CUSTOMERUSER" },
                    { "eb708a75 - d290 - 4195 - 9058 - b6d24bf26626", "ba6d315f-ce5c-4d4e-a713-2f8683911540", "SystemAdmin", "SYSTEMADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "382601e0 - 8d06 - 4bc6 - 9a04 - c9d5b169aa89");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "577fab6a - 4196 - 4b7a - b8dd - 6a8a08a78a1c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb708a75 - d290 - 4195 - 9058 - b6d24bf26626");
        }
    }
}
