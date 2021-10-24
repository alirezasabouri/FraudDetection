using FraudDetection.Adapter.Database.Repositories;
using FraudDetection.Contracts.Ports;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FraudDetection.Adapter.Database
{
    public static class DatabaseAdapter
    {
        public static IServiceCollection RegisterDatabaseAdapter(this IServiceCollection services)
        {
            services.AddScoped<IRuleEngineConfigurationsRepository, RuleEngineConfigurationsRepository>();

            return services;
        }

    }
}
