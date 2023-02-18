using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalChipization.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationAnimalToLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChippingLocationId",
                table: "Animals",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_ChippingLocationId",
                table: "Animals",
                column: "ChippingLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Locations_ChippingLocationId",
                table: "Animals",
                column: "ChippingLocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Locations_ChippingLocationId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_ChippingLocationId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "ChippingLocationId",
                table: "Animals");
        }
    }
}
