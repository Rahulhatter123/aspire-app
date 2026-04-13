using Clean_Architecture_Sample.Infrastructure.Data;
using Clean_Architecture_Sample.Domain.Interfaces;
using Clean_Architecture_Sample.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean_Architecture_Sample.Infrastructure.Extensions;

public static class DataConnectionExtension
{
    public static IServiceCollection AddDataConnection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("dbCon")));

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}