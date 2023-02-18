using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Core.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class AllElementsUniqueAttribute : ValidationAttribute
{
    public AllElementsUniqueAttribute() : base("All elements must be unique")
    {
    }


    public override bool IsValid(object? value)
    {
        if (value is not IEnumerable collection) return false;

        var listOfObjects = collection.Cast<object>().ToList();
        return listOfObjects.Distinct().Count() == listOfObjects.Count;
    }
}