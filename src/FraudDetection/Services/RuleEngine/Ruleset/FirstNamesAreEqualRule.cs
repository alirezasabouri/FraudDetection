using FraudDetection.Contracts.Ports;
using FraudDetection.Contracts.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudDetection.Services.RuleEngine.Ruleset
{
    public class FirstNamesAreEqualRule : IPersonComparisonRule
    {
        public Task<PersonComparisonRuleOutput> ExecuteAsync(PersonComparisonRuleInput input, IReadOnlyDictionary<PersonComparisonRuleType, RuleEngineConfiguration> config)
        {
            if (string.IsNullOrEmpty(input.SourcePerson.FirstName) ||
                string.IsNullOrEmpty(input.TargetPerson.FirstName))
                return Task.FromResult<PersonComparisonRuleOutput>(null);

            if (input.SourcePerson.FirstName.ToLower() == input.TargetPerson.FirstName.ToLower())
                return Task.FromResult(new PersonComparisonRuleOutput() {NewRank = input.CurrentRank + config[PersonComparisonRuleType.FirstNamesAreEqual].Weight });

            return Task.FromResult<PersonComparisonRuleOutput>(null);
        }
    }
}
