using FraudDetection.Contracts.Ports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FraudDetection.Adapter.Database.Repositories
{
    public class RuleEngineConfigurationsRepository : IRuleEngineConfigurationsRepository
    {
        public async Task<IEnumerable<RuleEngineConfiguration>> GetRulesConfig()
        {
            return new List<RuleEngineConfiguration>()
            {
                new RuleEngineConfiguration(){ RuleType = Services.RuleEngine.PersonComparisonRuleType.IdentificationNumbersAreEqual, Weight = 1},
                new RuleEngineConfiguration(){ RuleType = Services.RuleEngine.PersonComparisonRuleType.FirstNamesAreEqual, Weight = .2m},
                new RuleEngineConfiguration(){ RuleType = Services.RuleEngine.PersonComparisonRuleType.FirstNamesAreSimilar, Weight = .15m},
                new RuleEngineConfiguration(){ RuleType = Services.RuleEngine.PersonComparisonRuleType.LastNamesAreEqual, Weight = .4m},
                new RuleEngineConfiguration(){ RuleType = Services.RuleEngine.PersonComparisonRuleType.DatesOfBirthAreEqual, Weight = .4m}
            };
        }
    }
}
