using System.ComponentModel.DataAnnotations;
using AnimalChipization.Data.Entities.Constants;

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
            Gender.Male or Gender.Female or Gender.Other => true,
            null => AllowNull,
            _ => false
        };
    }
}