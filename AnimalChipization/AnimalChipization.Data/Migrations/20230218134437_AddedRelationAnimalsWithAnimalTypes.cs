using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalChipization.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationAnimalsWithAnimalTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalTypesAnimals",
                columns: table => new
                {
                    AnimalId = table.Column<long>(type: "bigint", nullable: false),
                    AnimalTypeId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalTypesAnimals", x => new { x.AnimalId, x.AnimalTypeId });
                    table.ForeignKey(
                        name: "FK_AnimalTypesAnimals_AnimalsTypes_AnimalTypeId",
                        column: x => x.AnimalTypeId,
                        principalTable: "AnimalsTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalTypesAnimals_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalTypesAnimals_AnimalTypeId",
                table: "AnimalTypesAnimals",
                column: "AnimalTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalTypesAnimals");
        }
    }
}
