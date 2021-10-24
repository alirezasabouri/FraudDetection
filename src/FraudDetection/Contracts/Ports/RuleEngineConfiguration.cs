﻿using FraudDetection.Services.RuleEngine;

namespace FraudDetection.Contracts.Ports
{
    public class RuleEngineConfiguration
    {
        public PersonComparisonRuleType RuleType{ get; set; }
        public float Weight { get; set; }
    }
}