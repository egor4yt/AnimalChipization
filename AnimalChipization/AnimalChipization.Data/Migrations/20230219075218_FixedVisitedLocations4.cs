using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalChipization.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedVisitedLocations4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AnimalsVisitedLocations_AnimalId_LocationId",
                table: "AnimalsVisitedLocations");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalsVisitedLocations_AnimalId",
                table: "AnimalsVisitedLocations",
                column: "AnimalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AnimalsVisitedLocations_AnimalId",
                table: "AnimalsVisitedLocations");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalsVisitedLocations_AnimalId_LocationId",
                table: "AnimalsVisitedLocations",
                columns: new[] { "AnimalId", "LocationId" });
        }
    }
}
