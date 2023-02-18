using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Core.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class GenderAttribute : ValidationAttribute
{
    public GenderAttribute() : base("Gender has invalid value")
    {
    }

    public override bool IsValid(object? value)
    {
        return value?.ToString() switch
        {
            "MALE" or "FEMALE" or "OTHER" => true,
            _ => false
        };
    }
}