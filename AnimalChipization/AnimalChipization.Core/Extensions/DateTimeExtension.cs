namespace AnimalChipization.Core.Extensions;

public static class DateTimeExtension
{
    public static string ToIso8601String(this DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.ffffffzzz");
    }

    public static string? ToIso8601String(this DateTime? dateTime)
    {
        return dateTime?.ToString("yyyy-MM-ddTHH:mm:ss.ffffffzzz");
    }
}