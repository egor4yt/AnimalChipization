using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalChipization.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedVisitedLocations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AnimalTypesAnimals_AnimalId_AnimalTypeId",
                table: "AnimalTypesAnimals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AnimalTypesAnimals_AnimalId_AnimalTypeId",
                table: "AnimalTypesAnimals",
                columns: new[] { "AnimalId", "AnimalTypeId" });
        }
    }
}
