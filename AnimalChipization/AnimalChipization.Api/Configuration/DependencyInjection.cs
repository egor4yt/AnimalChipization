// using AnimalChipization.Data;

using AnimalChipization.Core.Extensions;
using AnimalChipization.Data;
using AnimalChipization.Mappers;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AssemblyRunner = AnimalChipization.Services.AssemblyRunner;

namespace AnimalChipization.Api.Configuration;

public static class DependencyInjection
{
    public static void ConfigureBuilder(this WebApplicationBuilder app)
    {
        ConfigureAuthorization(app.Services);
        ConfigureInfrastructure(app.Services);
        ConfigureServices(app.Services);
        ConfigureAutomapper(app.Services);
        ConfigureRepositories(app.Services);
    }

    private static void ConfigureAuthorization(IServiceCollection services)
    {
    }

    private static void ConfigureInfrastructure(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "AnimalChipization.Api",
                Description = "Animal Chipization API"
            });
        });
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        var servicesAssembly = typeof(AssemblyRunner).Assembly;
        services.RegisterServicesEndsWith("Service", servicesAssembly);
    }
    
    private static void ConfigureAutomapper(IServiceCollection services)
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.AddProfile(new ContractsToEntitiesMappingConfig());
            config.AddProfile(new EntitiesToContractsMappingConfig());
        });

        var mapper = mappingConfig.CreateMapper();
        services.AddSingleton(mapper);
    }

    private static void ConfigureRepositories(IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(x => x.UseNpgsql(AppConfiguration.DatabaseConnectionString));
        var repositoriesAssembly = typeof(Data.AssemblyRunner).Assembly;
        services.RegisterServicesEndsWith("Repository", repositoriesAssembly);
    }
}