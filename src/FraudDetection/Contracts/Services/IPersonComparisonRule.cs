using FraudDetection.Contracts.Ports;
using FraudDetection.Services.RuleEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudDetection.Contracts.Services
{
    public interface IPersonComparisonRule
    {
        Task<PersonComparisonRuleOutput> ExecuteAsync(PersonComparisonRuleInput input, IReadOnlyDictionary<PersonComparisonRuleType, RuleEngineConfiguration> config);
    }

}
