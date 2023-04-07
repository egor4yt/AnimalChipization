using System.Globalization;
using NetTopologySuite.Geometries;

namespace AnimalChipization.Core.Helpers;

public static class GeometryHelper
{
    public static Polygon PolygonFromString(string str)
    {
        var pointsForPolygon = str
            .Split(";")
            .Select(coordinates =>
                new Coordinate(
                    double.Parse(coordinates.Split(",")[0], CultureInfo.InvariantCulture),
                    double.Parse(coordinates.Split(",")[1], CultureInfo.InvariantCulture)
                ))
            .ToList();
        pointsForPolygon.Add(pointsForPolygon[0]);
        var linearRing = new LinearRing(pointsForPolygon.ToArray());
        return new Polygon(linearRing);
    }
}