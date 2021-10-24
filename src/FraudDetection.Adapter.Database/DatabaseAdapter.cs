using FraudDetection.Adapter.Database.Repositories;
using FraudDetection.Contracts.Ports;
using FraudDetection.Misc;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace FraudDetection.Adapter.Database
{
    public static class DatabaseAdapter
    {
        public static IServiceCollection RegisterDatabaseAdapter(this IServiceCollection services, DatabaseSettings dbSettings)
        {
            services.AddSingleton<IDBConnectionFactory>(new MySqlConnectionFactory(dbSettings.ConnectionString));

            services.AddScoped<IRuleEngineConfigurationsRepository, RuleEngineConfigurationsRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            return services;
        }

    }
}
