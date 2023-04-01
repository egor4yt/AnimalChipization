using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Core.Validation;

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
            "ADMIN" or "CHIPPER" or "USER" => true,
            null => AllowNull,
            _ => false
        };
    }
}