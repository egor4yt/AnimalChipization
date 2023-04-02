using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Api.Contracts.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class GenderAttribute : ValidationAttribute
{
    public GenderAttribute(bool allowNull) : base("Gender has invalid value")
    {
        AllowNull = allowNull;
    }

    private bool AllowNull { get; }

    public override bool IsValid(object? value)
    {
        return value?.ToString() switch
        {
            "MALE" or "FEMALE" or "OTHER" => true,
            null => AllowNull,
            _ => false
        };
    }
}