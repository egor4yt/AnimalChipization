using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalChipization.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedVisitedLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AnimalTypesAnimals_AnimalId_AnimalTypeId",
                table: "AnimalTypesAnimals",
                columns: new[] { "AnimalId", "AnimalTypeId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AnimalTypesAnimals_AnimalId_AnimalTypeId",
                table: "AnimalTypesAnimals");
        }
    }
}
