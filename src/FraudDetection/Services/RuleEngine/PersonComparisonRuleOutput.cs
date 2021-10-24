using FraudDetection.Models;

namespace FraudDetection.Services.RuleEngine
{
    public class PersonComparisonRuleOutput
    {
        public float NewRank { get; set; }
        public bool BreakCircuit { get; set; }
    }

}
