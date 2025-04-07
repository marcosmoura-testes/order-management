using Application.Interfaces;
using Application.UseCases;
using Domain.Interfaces.Services.Supplier;
using Domain.UoW;
using Infrastructure;
using Infrastructure.Services.Supplier;
using Infrastructure.UoW;
using Microsoft.Azure.Functions.Worker;
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
                   .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                  .AddEnvironmentVariables();
        })
        .ConfigureFunctionsWebApplication()
        .ConfigureServices((context, services) =>
        {
            var configuration = context.Configuration;
            services.AddInfraDataSetup(configuration);
            services.AddScoped<IRequestOrderUseCase, RequestOrderUseCase>();

            services.AddApplicationInsightsTelemetryWorkerService();
            services.ConfigureFunctionsApplicationInsights();
           
        })
        .Build();

        host.Run();
    }
}