using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalChipization.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static void RegisterServicesEndsWith(this IServiceCollection services, string value, Assembly assembly)
    {
        var classes = assembly.GetTypes().Where(x => x.Name.EndsWith(value) && x.IsClass);

        foreach (var @class in classes)
        {
            var @interface = @class.GetInterfaces().First(x => x.Name.EndsWith(value));
            services.AddTransient(@interface, @class);
        }
    }
}