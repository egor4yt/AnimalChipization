using System.ComponentModel.DataAnnotations;
using AnimalChipization.Data.Entities.Constants;

namespace AnimalChipization.Api.Contracts.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class RoleAttribute : ValidationAttribute
{
    public RoleAttribute(bool allowNull) : base("Role has invalid value")
    {
        AllowNull = allowNull;
    }

    private bool AllowNull { get; }

    public override bool IsValid(object? value)
    {
        return value?.ToString() switch
        {
            AccountRole.Administrator or AccountRole.Chipper or AccountRole.User => true,
            null => AllowNull,
            _ => false
        };
    }
}