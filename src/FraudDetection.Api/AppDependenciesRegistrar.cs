using FraudDetection.Contracts.Services;
using FraudDetection.Contracts.Usecases;
using FraudDetection.Services;
using FraudDetection.Services.RuleEngine;
using FraudDetection.Services.RuleEngine.Ruleset;
using FraudDetection.Usecases;
using Microsoft.Extensions.DependencyInjection;

namespace FraudDetection.Api
{
    public static class AppDependenciesRegistrar
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection services)
        {
            services.AddScoped<IPersonUsecase, PersonUsecase>();
            services.AddScoped<IDuplicatePersonUsecase, DuplicatePersonUsecase>();
            services.AddScoped<INameSimilarityCheck, NameSimilarityCheck>();

            var serviceProvicer = services.BuildServiceProvider();

            var ruleEngineDefaultFactory = new PersonComparisonRuleEngineFactory();
            ruleEngineDefaultFactory
                .AddRule(new FirstNamesAreEqualRule())
                .AddRule(new FirstNamesAreSimilarRule(serviceProvicer.GetService<INameSimilarityCheck>()))
                .AddRule(new LastNamesAreEqualRule())
                .AddRule(new IdentificationNumbersAreEqualRule())
                .AddRule(new BirthDatesAreEqualRule());
            services.AddSingleton<IPersonComparisonRuleEngineFactory>(ruleEngineDefaultFactory);

            return services;
        }
    }
}
