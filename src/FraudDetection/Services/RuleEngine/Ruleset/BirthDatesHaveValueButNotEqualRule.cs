using FraudDetection.Contracts.Ports;
using FraudDetection.Contracts.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudDetection.Services.RuleEngine.Ruleset
{
    public class BirthDatesHaveValueButNotEqualRule : IPersonComparisonRule
    {
        public Task<PersonComparisonRuleOutput> ExecuteAsync(PersonComparisonRuleInput input, IReadOnlyDictionary<PersonComparisonRuleType, RuleEngineConfiguration> config)
        {
            if (input.SourcePerson.DateOfBirth.HasValue &&
                input.TargetPerson.DateOfBirth.HasValue &&
                input.SourcePerson.DateOfBirth.Value.Date != input.TargetPerson.DateOfBirth.Value.Date)
                return Task.FromResult(new PersonComparisonRuleOutput() { NewRank = 0, BreakCircuit = true });

            return Task.FromResult<PersonComparisonRuleOutput>(null);
        }
    }


}
