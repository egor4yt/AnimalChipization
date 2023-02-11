using AnimalChipization.Data;
using AnimalChipization.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AnimalChipization.Api.Configuration;

public static class DependencyInjection
{
    public static void ConfigureBuilder(this WebApplicationBuilder app)
    {
        ConfigureInfrastructure(app.Services);
        ConfigureRepositories(app.Services);
        ConfigureServices(app.Services);
    }

    private static void ConfigureInfrastructure(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
    }

    private static void ConfigureRepositories(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(x => x.UseNpgsql(AppConfiguration.DatabaseConnectionString));

        var repositoriesAssembly = typeof(IAccountRepository).Assembly;
        var repositories = repositoriesAssembly.GetTypes().Where(x => x.Name.EndsWith("Repository") && x.IsClass);

        foreach (var repository in repositories)
        {
            var repositoryInterface = repository.GetInterfaces().First(x => x.Name.EndsWith("Repository"));
            services.AddScoped(repositoryInterface, repository);
        }
    }
}