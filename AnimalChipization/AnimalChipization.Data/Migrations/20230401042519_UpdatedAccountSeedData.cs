using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AnimalChipization.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAccountSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 1, 4, 25, 19, 625, DateTimeKind.Utc).AddTicks(390));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 2, new DateTime(2023, 4, 1, 4, 25, 19, 625, DateTimeKind.Utc).AddTicks(610), "chipper@simbirsoft.com", "chipperFirstName", "chipperLastName", "DAAAD6E5604E8E17BD9F108D91E26AFE6281DAC8FDA0091040A7A6D7BD9B43B5", "CHIPPER" },
                    { 3, new DateTime(2023, 4, 1, 4, 25, 19, 625, DateTimeKind.Utc).AddTicks(630), "user@simbirsoft.com", "userFirstName", "userLastName", "DAAAD6E5604E8E17BD9F108D91E26AFE6281DAC8FDA0091040A7A6D7BD9B43B5", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 4, 1, 4, 20, 37, 97, DateTimeKind.Utc).AddTicks(3680));
        }
    }
}
