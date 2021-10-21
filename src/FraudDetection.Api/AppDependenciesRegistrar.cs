using FraudDetection.Contracts.Usecases;
using FraudDetection.Usecases;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FraudDetection.Api
{
    public static class AppDependenciesRegistrar
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPersonUsecase, PersonUsecase>();

            return services;
        }
    }
}
