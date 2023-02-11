namespace AnimalChipization.Api.Configuration;

public static class MiddlewaresConfiguration
{
    public static void ConfigureMiddlewares(this WebApplication app)
    {
        app.ConfigureSharedMiddlewares();
        app.ConfigureDevelopmentMiddlewares();
        app.ConfigureProductionMiddlewares();
    }

    private static void ConfigureSharedMiddlewares(this WebApplication app)
    {
        
    }
    
    private static void ConfigureDevelopmentMiddlewares(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return;
        
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseDeveloperExceptionPage();
    }
    
    private static void ConfigureProductionMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment()) return;
    }
}