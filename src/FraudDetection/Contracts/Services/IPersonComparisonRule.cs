using FraudDetection.Contracts.Ports;
using FraudDetection.Services.RuleEngine;

namespace FraudDetection.Contracts.Services
{
    public interface IPersonComparisonRule
    {
        bool IsTrue(PersonComparisonRuleInput input);
    }

}
