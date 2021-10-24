using FraudDetection.Contracts.Services;

namespace FraudDetection.Services.RuleEngine.Ruleset
{
    public class FirstNamesAreEqualRule : IPersonComparisonRule
    {
        public bool IsTrue(PersonComparisonRuleInput input)
        {
            if (string.IsNullOrEmpty(input.SourcePerson.FirstName) ||
                string.IsNullOrEmpty(input.TargetPerson.FirstName))
                return false;

            if (input.SourcePerson.FirstName.ToLower() == input.TargetPerson.FirstName.ToLower())
                return true;

            return false;
        }
    }
}
