using FraudDetection.Contracts.Services;

namespace FraudDetection.Services.RuleEngine.Ruleset
{
    public class FirstNamesAreSimilarRule : IPersonComparisonRule
    {
        private readonly INameSimilarityCheck _nameSimilarityCheck;

        public FirstNamesAreSimilarRule(INameSimilarityCheck nameSimilarityCheck)
        {
            _nameSimilarityCheck = nameSimilarityCheck;
        }

        public bool IsTrue(PersonComparisonRuleInput input)
        {
            if (string.IsNullOrEmpty(input.SourcePerson.FirstName) ||
                string.IsNullOrEmpty(input.TargetPerson.FirstName))
                return false;

            if (input.SourcePerson.FirstName.ToLower() == input.TargetPerson.FirstName.ToLower())
                return false;

            if (_nameSimilarityCheck.AreSimilar(input.SourcePerson.FirstName, input.TargetPerson.FirstName))
                return true;

            return false;
        }
    }
}
