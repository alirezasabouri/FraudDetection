using FraudDetection.Contracts.Ports;
using FraudDetection.Contracts.Services;
using FraudDetection.Services.RuleEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudDetection.UnitTests.Dummies
{
    public class DummyComparisonRule : IPersonComparisonRule
    {
        private PersonComparisonRuleOutput _personComparisonRuleOutput;

        public DummyComparisonRule(PersonComparisonRuleOutput personComparisonRuleOutput)
        {
            _personComparisonRuleOutput = personComparisonRuleOutput;
        }

        public Task<PersonComparisonRuleOutput> ExecuteAsync(PersonComparisonRuleInput input, 
            IReadOnlyDictionary<PersonComparisonRuleType,
            RuleEngineConfiguration> config)
        {
            return Task.FromResult(_personComparisonRuleOutput);
        }
    }
}
