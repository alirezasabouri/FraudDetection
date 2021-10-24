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
        public async Task<Result<float>> FindSimilarityRankAsync(Person person1, Person person2)
        {
            var ruleEngineConfigs = await _ruleEngineConfigurationRepository.GetRulesConfig();
            var ruleEngineConfigsDictionary = ruleEngineConfigs.ToDictionary(k => k.RuleType);

            float result = 0;
            var ruleset = _personComparisonRuleEngineFactory.GetRuleSet();

            foreach (var ruleType in ruleset.Keys)
            {
                var ruleIsApplicable = ruleset[ruleType].IsTrue(new PersonComparisonRuleInput(person1, person2));
                if (ruleIsApplicable)
                    result += ruleEngineConfigsDictionary[ruleType].Weight;
                if (result >= 1)
                    break;
            }

            return Result.Success(result);

            //if (!string.IsNullOrEmpty(person1.IdentificationNumber) &&
            //    !string.IsNullOrEmpty(person2.IdentificationNumber) &&
            //    person1.IdentificationNumber.ToLower() == person2.IdentificationNumber.ToLower())
            //{
            //    result = 1;
            //    return Task.FromResult(Result.Success(result));
            //}

            //if (person1.LastName.ToLower() == person2.LastName.ToLower())
            //    result += 0.4f;

            //if (person1.FirstName.ToLower() == person2.FirstName.ToLower())
            //    result += 0.2f;
            //else if (_nameSimilarityCheck.AreSimilar(person1.FirstName, person2.FirstName))
            //    result += 0.15f;

            //if (person1.DateOfBirth.HasValue &&
            //    person2.DateOfBirth.HasValue &&
            //    person1.DateOfBirth.Value.Date == person2.DateOfBirth.Value.Date)
            //    result += 0.4f;

            //return Task.FromResult(Result.Success(result));
        }
    }
}
