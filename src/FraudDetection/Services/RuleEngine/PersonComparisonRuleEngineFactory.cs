using FraudDetection.Contracts.Services;
using System.Collections.Generic;

namespace FraudDetection.Services.RuleEngine
{
    public class PersonComparisonRuleEngineFactory : IPersonComparisonRuleEngineFactory
    {
        private Dictionary<PersonComparisonRuleType, IPersonComparisonRule> _rulesetDictionary;

        public PersonComparisonRuleEngineFactory()
        {
            _rulesetDictionary = new Dictionary<PersonComparisonRuleType, IPersonComparisonRule>();
        }

        public PersonComparisonRuleEngineFactory Configure(PersonComparisonRuleType ruleType, IPersonComparisonRule rule)
        {
            if (_rulesetDictionary.ContainsKey(ruleType))
                _rulesetDictionary[ruleType] = rule;
            else
                _rulesetDictionary.Add(ruleType, rule);

            return this;
        }

        public IReadOnlyDictionary<PersonComparisonRuleType, IPersonComparisonRule> GetRuleSet()
        {
            return _rulesetDictionary;
        }
    }

}
