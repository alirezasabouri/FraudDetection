using FraudDetection.Models;

namespace FraudDetection.Services.RuleEngine
{
    public class PersonComparisonRuleInput
    {
        public decimal CurrentRank { get; set; }
        public Person SourcePerson { get; private set; }
        public Person TargetPerson { get; private set; }

        public PersonComparisonRuleInput(Person sourcePerson, Person targetPerson, decimal currentRank)
        {
            CurrentRank = currentRank;
            SourcePerson = sourcePerson;
            TargetPerson = targetPerson;
        }
    }

}
