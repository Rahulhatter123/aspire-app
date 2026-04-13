using Clean_Architecture_Sample.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clean_Architecture_Sample.ProductIntegrationTest;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Set the environment to "Testing" so it looks for appsettings.Testing.json first
        //builder.UseEnvironment("Development");

        builder.ConfigureServices((context, services) =>
        {
            // 1. Remove the existing DbContext registration (from the real Program.cs)
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DataDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // 2. Retrieve the connection string from Configuration (appsettings)
            // It expects: "ConnectionStrings": { "dbCon": "..." }
            var connectionString = context.Configuration.GetConnectionString("dbCon");

            // 3. Register the DbContext using the actual SQL Server connection
            services.AddDbContext<DataDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // 4. Prepare the Database Schema
            // This section ensures the database is in a clean state before tests run
            var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DataDbContext>();

            // WARNING: EnsureDeleted() drops the database. 
            // Only use this if 'dbCon' points to a dedicated Test Database.
            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
        });
    }
}