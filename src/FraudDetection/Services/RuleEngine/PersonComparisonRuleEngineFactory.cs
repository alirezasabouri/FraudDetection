using FraudDetection.Contracts.Services;
using System.Collections.Generic;

namespace FraudDetection.Services.RuleEngine
{
    public class PersonComparisonRuleEngineFactory : IPersonComparisonRuleEngineFactory
    {
        private HashSet<IPersonComparisonRule> _ruleset;

        public PersonComparisonRuleEngineFactory()
        {
            _ruleset = new HashSet<IPersonComparisonRule>();
        }

        public PersonComparisonRuleEngineFactory AddRule(IPersonComparisonRule rule)
        {
            _ruleset.Add(rule);
            return this;
        }

        public ISet<IPersonComparisonRule> GetRuleSet()
        {
            return _ruleset;
        }
    }

}
