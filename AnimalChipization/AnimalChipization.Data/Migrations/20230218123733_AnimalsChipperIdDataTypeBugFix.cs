using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimalChipization.Data.Migrations
{
    /// <inheritdoc />
    public partial class AnimalsChipperIdDataTypeBugFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChipperId",
                table: "Animals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_ChipperId",
                table: "Animals",
                column: "ChipperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Accounts_ChipperId",
                table: "Animals",
                column: "ChipperId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Accounts_ChipperId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_ChipperId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "ChipperId",
                table: "Animals");
        }
    }
}
