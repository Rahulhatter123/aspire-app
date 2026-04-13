using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean_Architecture_Sample.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationExtension(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(assembly));

        services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

        return services;
    }
}