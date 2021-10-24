using FraudDetection.Contracts.Services;

namespace FraudDetection.Services.RuleEngine.Ruleset
{
    public class IdentificationNumbersAreEqualRule : IPersonComparisonRule
    {
        public bool IsTrue(PersonComparisonRuleInput input)
        {
            if (string.IsNullOrEmpty(input.SourcePerson.IdentificationNumber) ||
                string.IsNullOrEmpty(input.TargetPerson.IdentificationNumber))
                return false;

            if (input.SourcePerson.IdentificationNumber.ToLower() == input.TargetPerson.IdentificationNumber.ToLower())
                return true;

            return false;
        }
    }
}
