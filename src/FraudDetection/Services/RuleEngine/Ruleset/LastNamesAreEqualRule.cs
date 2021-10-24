using FraudDetection.Contracts.Ports;
using FraudDetection.Contracts.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace FraudDetection.Services.RuleEngine.Ruleset
{
    public class LastNamesAreEqualRule : IPersonComparisonRule
    {
        public Task<PersonComparisonRuleOutput> ExecuteAsync(PersonComparisonRuleInput input, IReadOnlyDictionary<PersonComparisonRuleType, RuleEngineConfiguration> config)
        {
            if (string.IsNullOrEmpty(input.SourcePerson.LastName) ||
                string.IsNullOrEmpty(input.TargetPerson.LastName))
                return Task.FromResult<PersonComparisonRuleOutput>(null);

            if (input.SourcePerson.LastName.ToLower() == input.TargetPerson.LastName.ToLower())
                return Task.FromResult(new PersonComparisonRuleOutput() { NewRank = input.CurrentRank + config[PersonComparisonRuleType.LastNamesAreEqual].Weight });

            return Task.FromResult<PersonComparisonRuleOutput>(null);
        }
    }
}
