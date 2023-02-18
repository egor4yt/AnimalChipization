using System.ComponentModel.DataAnnotations;

namespace AnimalChipization.Core.Validation;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class GreaterThanAttribute : ValidationAttribute
{
    private readonly object _targetValue;

    public GreaterThanAttribute(float targetValue) : base($"Must be more than {targetValue}")
    {
        _targetValue = targetValue;
    }

    public GreaterThanAttribute(int targetValue): base($"Must be more than {targetValue}")
    {
        _targetValue = targetValue;
    }
    
    public GreaterThanAttribute(long targetValue): base($"Must be more than {targetValue}")
    {
        _targetValue = targetValue;
    }

    public override bool IsValid(object? value)
    {
        return value?.GetType().Name.ToLower() switch
        {
            "float" or "single" => Convert.ToSingle(value) > Convert.ToSingle(_targetValue),
            "double" => Convert.ToDouble(value) > Convert.ToDouble(_targetValue),
            "int32" => Convert.ToInt32(value) > Convert.ToInt32(_targetValue),
            "int64" => Convert.ToInt64(value) > Convert.ToInt64(_targetValue),
            "decimal" => Convert.ToDecimal(value) > Convert.ToDecimal(_targetValue),
            _ => false
        };
    }
}