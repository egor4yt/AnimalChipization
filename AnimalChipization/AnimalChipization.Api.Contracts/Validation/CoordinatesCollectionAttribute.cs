using System.Collections;
using System.ComponentModel.DataAnnotations;
using AnimalChipization.Api.Contracts.Shared;

namespace AnimalChipization.Api.Contracts.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class CoordinatesCollectionAttribute : ValidationAttribute
{
    public CoordinatesCollectionAttribute() : base("Collection has invalid values")
    {
    }


    public override bool IsValid(object? value)
    {
        if (value is not IEnumerable collection) return false;
        var listOfObjects = collection.Cast<CoordinatesRequestItem>().ToList();

        var allValid = true;
        allValid = allValid && listOfObjects.Count >= 3;
        allValid = allValid && listOfObjects.Any(x => x == null) == false;
        allValid = allValid && listOfObjects.Select(x => x.Latitude).Distinct().Count() != 1;
        allValid = allValid && listOfObjects.Select(x => x.Longitude).Distinct().Count() != 1;
        allValid = allValid && listOfObjects.DistinctBy(x => new { x.Latitude, x.Longitude }).Count() == listOfObjects.Count;
        // todo: проверять что фигура не пересекает сама себя

        return allValid;
    }
}