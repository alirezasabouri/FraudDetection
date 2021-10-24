using FraudDetection.Contracts.Ports;
using FraudDetection.Contracts.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudDetection.Services.RuleEngine.Ruleset
{
    public class IdentificationNumbersAreEqualRule : IPersonComparisonRule
    {
        public Task<PersonComparisonRuleOutput> ExecuteAsync(PersonComparisonRuleInput input, IReadOnlyDictionary<PersonComparisonRuleType, RuleEngineConfiguration> config)
        {
            if (string.IsNullOrEmpty(input.SourcePerson.IdentificationNumber) ||
                string.IsNullOrEmpty(input.TargetPerson.IdentificationNumber))
                return Task.FromResult<PersonComparisonRuleOutput>(null);

            if (input.SourcePerson.IdentificationNumber.ToLower() == input.TargetPerson.IdentificationNumber.ToLower())
                return Task.FromResult(new PersonComparisonRuleOutput() { NewRank = 1, BreakCircuit = true });

            return Task.FromResult<PersonComparisonRuleOutput>(null);
        }
    }
}
