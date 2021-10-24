using FraudDetection.Contracts.Services;

namespace FraudDetection.Services.RuleEngine.Ruleset
{
    public class BirthDatesAreEqualRule : IPersonComparisonRule
    {
        public bool IsTrue(PersonComparisonRuleInput input)
        {
            if (input.SourcePerson.DateOfBirth.HasValue &&
                input.TargetPerson.DateOfBirth.HasValue &&
                input.SourcePerson.DateOfBirth.Value.Date == input.TargetPerson.DateOfBirth.Value.Date)
                return true;

            return false;
        }
    }

   
}
