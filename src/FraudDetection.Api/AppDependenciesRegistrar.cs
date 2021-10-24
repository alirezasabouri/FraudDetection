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
                .Configure(PersonComparisonRuleType.FirstNamesAreEqual, new FirstNamesAreEqualRule())
                .Configure(PersonComparisonRuleType.FirstNamesAreSimilar, new FirstNamesAreSimilarRule(serviceProvicer.GetService<INameSimilarityCheck>()))
                .Configure(PersonComparisonRuleType.LastNamesAreEqual, new LastNamesAreEqualRule())
                .Configure(PersonComparisonRuleType.IdentificationNumbersAreEqual, new IdentificationNumbersAreEqualRule())
                .Configure(PersonComparisonRuleType.DatesOfBirthAreEqual, new BirthDatesAreEqualRule());
            services.AddSingleton<IPersonComparisonRuleEngineFactory>(ruleEngineDefaultFactory);

            return services;
        }
    }
}
