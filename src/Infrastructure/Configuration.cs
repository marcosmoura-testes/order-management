using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.UoW;
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

            return services;
        }
    }
}
