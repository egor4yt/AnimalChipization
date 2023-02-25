namespace AnimalChipization.Core.Extensions;

public static class DateTimeExtension
{
    public static string ToIso8601String(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
    }
    
    public static string? ToIso8601String(this DateTime? dateTime)
    {
        return dateTime?.ToString("yyyy-MM-ddTHH:mm:ssZ");
    }
}