using FraudDetection.Models;

namespace FraudDetection.Services.RuleEngine
{
    public class PersonComparisonRuleInput
    {
        public Person SourcePerson { get; private set; }
        public Person TargetPerson { get; private set; }

        public PersonComparisonRuleInput(Person sourcePerson, Person targetPerson)
        {
            SourcePerson = sourcePerson;
            TargetPerson = targetPerson;
        }
    }

}
