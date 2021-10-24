using FraudDetection.Services.RuleEngine;
using System.Collections.Generic;

namespace FraudDetection.Contracts.Services
{
    public interface IPersonComparisonRuleEngineFactory
    {
        ISet<IPersonComparisonRule> GetRuleSet();
    }
}
