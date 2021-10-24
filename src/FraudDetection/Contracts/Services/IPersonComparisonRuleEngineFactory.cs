using FraudDetection.Services.RuleEngine;
using System.Collections.Generic;

namespace FraudDetection.Contracts.Services
{
    public interface IPersonComparisonRuleEngineFactory
    {
        IReadOnlyDictionary<PersonComparisonRuleType, IPersonComparisonRule> GetRuleSet();
    }
}
