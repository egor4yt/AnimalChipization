using System.ComponentModel.DataAnnotations;
using AnimalChipization.Data.Entities.Constants;

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
            LifeStatus.Alive or LifeStatus.Dead => true,
            null => AllowNull,
            _ => false
        };
    }
}