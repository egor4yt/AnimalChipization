using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalChipization.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static void RegisterServicesEndsWith(this IServiceCollection services, string value, Assembly assembly)
    {
        var repositories = assembly.GetTypes().Where(x => x.Name.EndsWith(value) && x.IsClass);

        foreach (var repository in repositories)
        {
            var repositoryInterface = repository.GetInterfaces().First(x => x.Name.EndsWith(value));
            services.AddTransient(repositoryInterface, repository);
        }
    }
}