using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Core.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class ListPositiveLongNumbersAttribute : ValidationAttribute
{
    public ListPositiveLongNumbersAttribute() : base("All numbers must be more than 0")
    {
    }


    public override bool IsValid(object? value)
    {
        return value is ICollection<long> collection && collection.Any(x => x <= 0) == false;
    }
}