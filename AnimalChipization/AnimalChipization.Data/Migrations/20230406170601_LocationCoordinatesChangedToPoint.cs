using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace AnimalChipization.Data.Migrations
{
    /// <inheritdoc />
    public partial class LocationCoordinatesChangedToPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Locations");

            migrationBuilder.AddColumn<Point>(
                name: "Point",
                table: "Locations",
                type: "geometry",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Point",
                table: "Locations");

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Locations",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Locations",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
