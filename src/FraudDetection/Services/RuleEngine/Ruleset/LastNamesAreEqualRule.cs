using FraudDetection.Contracts.Services;

namespace FraudDetection.Services.RuleEngine.Ruleset
{
    public class LastNamesAreEqualRule : IPersonComparisonRule
    {
        public bool IsTrue(PersonComparisonRuleInput input)
        {
            if (string.IsNullOrEmpty(input.SourcePerson.LastName) ||
                string.IsNullOrEmpty(input.TargetPerson.LastName))
                return false;

            if (input.SourcePerson.LastName.ToLower() == input.TargetPerson.LastName.ToLower())
                return true;

            return false;
        }
    }
}
