using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AnimalChipization.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedAnimals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_Email",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalType",
                table: "AnimalType");

            migrationBuilder.RenameTable(
                name: "AnimalType",
                newName: "AnimalsTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalsTypes",
                table: "AnimalsTypes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WeightKilograms = table.Column<float>(type: "real", nullable: false),
                    HeightMeters = table.Column<float>(type: "real", nullable: false),
                    LengthMeters = table.Column<float>(type: "real", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    LifeStatus = table.Column<string>(type: "text", nullable: false),
                    ChippingDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeathDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalsTypes",
                table: "AnimalsTypes");

            migrationBuilder.RenameTable(
                name: "AnimalsTypes",
                newName: "AnimalType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalType",
                table: "AnimalType",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                table: "Accounts",
                column: "Email",
                unique: true);
        }
    }
}
