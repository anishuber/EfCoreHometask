using Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UI
{
    internal static class Program
    {
        static async Task Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("CarProductionDb")
                ?? throw new InvalidOperationException("Connection string \"CarProductionDb\" was not found.");

            var services = new ServiceCollection();

            services.AddCarProductionServices(connectionString);

            await using var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<CarProductionDbContext>();
            await DataInitializer.InitializeAsync(dbContext);

            var app = scope.ServiceProvider.GetRequiredService<ConsoleApplication>();
            await app.RunAsync();
        }
    }
}
