using FraudDetection.Models;

namespace FraudDetection.Services.RuleEngine
{
    public class PersonComparisonRuleOutput
    {
        public decimal NewRank { get; set; }
        public bool BreakCircuit { get; set; }
    }

}
