using FraudDetection.Services.RuleEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FraudDetection.Contracts.Ports
{
    public interface IRuleEngineConfigurationsRepository
    {
        Task<IEnumerable<RuleEngineConfiguration>> GetRulesConfig();
    }
}
