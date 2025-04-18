using Domain.UoW;
using Infrastructure;
using Infrastructure.UoW;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = new HostBuilder()
        .ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("host.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("local.settings", optional: true, reloadOnChange: true)
                  .AddEnvironmentVariables();
        })
        .ConfigureFunctionsWebApplication()
        .ConfigureServices((context, services) =>
        {
            var configuration = context.Configuration;
            services.AddDbContext<DefaultDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddApplicationInsightsTelemetryWorkerService();
            services.ConfigureFunctionsApplicationInsights();
            services.AddHttpClient("SupplierApi", client =>
            {
                client.BaseAddress = new Uri(configuration["supplierApiUrl"]);
                client.Timeout = TimeSpan.FromSeconds(10);
            });
        })
        .Build();

        host.Run();
    }
}