using Azure.Messaging.ServiceBus;
using Domain.Interfaces.Services.Supplier;
using Domain.UoW;
using Infrastructure.Services.Supplier;
using Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Configuration
    {
        public static IServiceCollection AddInfraDataSetup(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration["ConexaoMySql:MySqlConnectionString"];
            services.AddDbContext<DefaultDbContext>(options =>
                 options.UseSqlServer(connection)
            );

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ISupplierService, SupplierService>();

            var serviceBusConnectionString = configuration["ServiceBus:ConnectionString"];
            services.AddSingleton<ServiceBusClient>(new ServiceBusClient(serviceBusConnectionString));

            return services;
        }
    }
}
