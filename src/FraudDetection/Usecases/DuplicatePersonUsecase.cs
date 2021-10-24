using FraudDetection.Contracts.Ports;
using FraudDetection.Contracts.Services;
using FraudDetection.Contracts.Usecases;
using FraudDetection.Misc;
using FraudDetection.Models;
using FraudDetection.Services.RuleEngine;
using System.Linq;
using System.Threading.Tasks;

namespace FraudDetection.Usecases
{
    public class DuplicatePersonUsecase : IDuplicatePersonUsecase
    {
        private readonly IRuleEngineConfigurationsRepository _ruleEngineConfigurationRepository;
        private readonly IPersonComparisonRuleEngineFactory _personComparisonRuleEngineFactory;

        public DuplicatePersonUsecase(IRuleEngineConfigurationsRepository ruleEngineConfigurationRepository,
            IPersonComparisonRuleEngineFactory personComparisonRuleEngineFactory)
        {
            _ruleEngineConfigurationRepository = ruleEngineConfigurationRepository;
            _personComparisonRuleEngineFactory = personComparisonRuleEngineFactory;
        }

        /// <inheritdoc />
        public async Task<Result<decimal>> FindSimilarityRankAsync(Person person1, Person person2)
        {
            var ruleEngineConfigs = await _ruleEngineConfigurationRepository.GetRulesConfig();
            var ruleEngineConfigsDictionary = ruleEngineConfigs.ToDictionary(k => k.RuleType);

            decimal result = 0;
            var ruleset = _personComparisonRuleEngineFactory.GetRuleSet();

            foreach (var rule in ruleset)
            {
                var ruleExecutionResult = await rule.ExecuteAsync(new PersonComparisonRuleInput(person1, person2, result), ruleEngineConfigsDictionary);

                if (ruleExecutionResult == null)
                    continue;

                result = ruleExecutionResult.NewRank;

                if (ruleExecutionResult.BreakCircuit)
                    break;
            }

            return Result.Success(result > 1 ? 1 : result);
        }
    }
}
