using System.Collections;
using System.ComponentModel.DataAnnotations;
using AnimalChipization.Api.Contracts.Shared;
using NetTopologySuite.Geometries;

namespace AnimalChipization.Api.Contracts.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class PolygonCoordinatesCollectionAttribute : ValidationAttribute
{
    public PolygonCoordinatesCollectionAttribute() : base("Collection has invalid values")
    {
    }


    public override bool IsValid(object? value)
    {
        if (value is not IEnumerable collection) return false;
        var listOfCoordinates = collection.Cast<CoordinatesRequestItem>().ToList();

        var allValid = true;
        allValid = allValid && listOfCoordinates.Count >= 3;
        allValid = allValid && listOfCoordinates.Any(x => x == null) == false;
        allValid = allValid && listOfCoordinates.DistinctBy(x => new { x.Latitude, x.Longitude }).Count() == listOfCoordinates.Count;
        allValid = allValid && IsAllPointsOnLine(listOfCoordinates) == false;
        allValid = allValid && HasInvalidLines(listOfCoordinates) == false;

        return allValid;
    }

    private static bool HasInvalidLines(IEnumerable<CoordinatesRequestItem> coordinatesRequestItems)
    {
        var coordinates = coordinatesRequestItems.Select(x => new Coordinate(x.Latitude, x.Longitude)).ToList();
        coordinates.Add(coordinates[0]);

        var storedLines = new List<LineString>();

        for (var i = 0; i < coordinates.Count - 1; i++)
        {
            var prevLine = storedLines.LastOrDefault();
            var line = new LineString(new[] { coordinates[i], coordinates[i + 1] });

            if (prevLine?.EndPoint.Equals(line.StartPoint) == false) return true;
            if (storedLines.Any(x => LinesIntersects(x, line))) return true;

            storedLines.Add(line);
        }

        return false;
    }

    private static bool LinesIntersects(LineString firstLine, LineString secondLine)
    {
        var intersection = firstLine.Intersection(secondLine);
        if (intersection.IsEmpty) return false;
        if (intersection is not Point intersectionPoint) return true;

        var extremePoints = new List<Point>
        {
            firstLine.StartPoint,
            firstLine.EndPoint,
            secondLine.EndPoint
        };

        return !extremePoints.Any(x => intersection.Equals(x));
    }

    private static bool IsAllPointsOnLine(IReadOnlyList<CoordinatesRequestItem> coordinates)
    {
        var startPoint = new Coordinate(coordinates[0].Latitude, coordinates[0].Longitude);
        var endPoint = new Coordinate(coordinates[1].Latitude, coordinates[1].Longitude);
        var slope = (endPoint.Y - startPoint.Y) / (endPoint.X - startPoint.X);
        var yIntercept = (startPoint.X * slope - startPoint.Y) * -1;

        return coordinates.Skip(2).All(x => IsPointOnLine(slope, yIntercept, new Coordinate(x.Latitude, x.Longitude)));
    }

    private static bool IsPointOnLine(double slope, double yIntercept, Coordinate coordinate)
    {
        return Math.Abs(coordinate.Y - (slope * coordinate.X + yIntercept)) < 0.01;
    }
}