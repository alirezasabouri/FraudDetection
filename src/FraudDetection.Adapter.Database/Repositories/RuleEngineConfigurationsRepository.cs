using FraudDetection.Contracts.Ports;
using FraudDetection.Services.RuleEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudDetection.Adapter.Database.Repositories
{
    public class RuleEngineConfigurationsRepository : IRuleEngineConfigurationsRepository
    {
        public async Task<IEnumerable<RuleEngineConfiguration>> GetRulesConfig()
        {
            return new List<RuleEngineConfiguration>()
            {
                new RuleEngineConfiguration(){ RuleType = PersonComparisonRuleType.IdentificationNumbersAreEqual, Weight = 1},
                new RuleEngineConfiguration(){ RuleType = PersonComparisonRuleType.FirstNamesAreEqual, Weight = .2m},
                new RuleEngineConfiguration(){ RuleType = PersonComparisonRuleType.FirstNamesAreSimilar, Weight = .15m},
                new RuleEngineConfiguration(){ RuleType = PersonComparisonRuleType.LastNamesAreEqual, Weight = .4m},
                new RuleEngineConfiguration(){ RuleType = PersonComparisonRuleType.DatesOfBirthAreEqual, Weight = .4m}
            };
        }
    }
}
