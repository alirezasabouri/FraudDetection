namespace FraudDetection.Services.RuleEngine
{
    public enum PersonComparisonRuleType
    {
        IdentificationNumbersAreEqual = 1,
        FirstNamesAreEqual = 2,
        FirstNamesAreSimilar = 3,
        LastNamesAreEqual = 4,
        DatesOfBirthAreEqual = 5,
        DatesOfBirthAreBothSetButNotEqual = 6,
    }
}
