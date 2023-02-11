namespace AnimalChipization.Api.Configuration;

public static class AppConfiguration
{
    private static ILogger<WebApplication> _logger = null!;

    /* Default values */
    private static string _databaseConnectionString =
        "host=localhost;port=5432;database=local-animal-chipization;username=user;password=password";


    public static string DatabaseConnectionString => _databaseConnectionString;


    public static void UpdateEnvironmentVariables(this WebApplication app)
    {
        // if (app.Environment.IsDevelopment()) return;

        _logger = app.Services.GetRequiredService<ILogger<WebApplication>>();

        TryUpdateVariable(ref _databaseConnectionString, "DATABASE_CONNECTION_STRING");
    }

    private static void TryUpdateVariable(ref string variable, string name)
    {
        var value = Environment.GetEnvironmentVariable(name);
        if (string.IsNullOrWhiteSpace(value)) _logger.LogWarning("Environment variable {name} not set", name);
        else variable = value;
    }
}