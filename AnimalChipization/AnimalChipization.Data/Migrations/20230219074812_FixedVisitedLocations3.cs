using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AnimalChipization.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedVisitedLocations3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalsVisitedLocations",
                table: "AnimalsVisitedLocations");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AnimalsVisitedLocations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalsVisitedLocations",
                table: "AnimalsVisitedLocations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalsVisitedLocations_AnimalId_LocationId",
                table: "AnimalsVisitedLocations",
                columns: new[] { "AnimalId", "LocationId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalsVisitedLocations",
                table: "AnimalsVisitedLocations");

            migrationBuilder.DropIndex(
                name: "IX_AnimalsVisitedLocations_AnimalId_LocationId",
                table: "AnimalsVisitedLocations");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "AnimalsVisitedLocations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalsVisitedLocations",
                table: "AnimalsVisitedLocations",
                columns: new[] { "AnimalId", "LocationId" });
        }
    }
}
