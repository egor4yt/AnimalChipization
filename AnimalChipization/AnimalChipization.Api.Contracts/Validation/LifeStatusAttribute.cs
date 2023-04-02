using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Api.Contracts.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class LifeStatusAttribute : ValidationAttribute
{
    public LifeStatusAttribute(bool allowNull) : base("Life status has invalid value")
    {
        AllowNull = allowNull;
    }

    private bool AllowNull { get; }

    public override bool IsValid(object? value)
    {
        return value?.ToString() switch
        {
            "ALIVE" or "DEAD" => true,
            null => AllowNull,
            _ => false
        };
    }
}