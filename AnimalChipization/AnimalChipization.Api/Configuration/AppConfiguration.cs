namespace AnimalChipization.Api.Configuration;

public static class AppConfiguration
{
    private static ILogger<WebApplication> _logger = null!;

    /* Default values */
    public static string DatabaseConnectionString { get; private set; } = "host=localhost;port=2222;database=animal-chipization;username=postgres;password=postgres";

    public static void UpdateEnvironmentVariables(this WebApplication app)
    {
        if (app.Environment.IsDevelopment()) return;

        _logger = app.Services.GetRequiredService<ILogger<WebApplication>>();

        DatabaseConnectionString = TryUpdateVariable(DatabaseConnectionString, "DATABASE_CONNECTION_STRING");
    }

    private static string TryUpdateVariable(string variable, string name)
    {
        var defaultValue = variable;
        var environmentValue = Environment.GetEnvironmentVariable(name);
        if (string.IsNullOrWhiteSpace(environmentValue)) _logger.LogWarning("Environment variable {name} not set", name);
        return environmentValue ?? defaultValue;
    }
}