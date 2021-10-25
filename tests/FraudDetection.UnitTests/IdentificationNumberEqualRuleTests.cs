using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using FraudDetection.Contracts.Ports;
using FraudDetection.Contracts.Services;
using FraudDetection.Models;
using FraudDetection.Services.RuleEngine;
using FraudDetection.Services.RuleEngine.Ruleset;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FraudDetection.UnitTests
{
    public class IdentificationNumberEqualRuleTests
    {
        private readonly IFixture _fixture;

        public IdentificationNumberEqualRuleTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GivenTwoPersonWithIdenticalIdNumber_WhenCompared_MustSetResultTo1AndBreakCircuit()
        {
            var ruleWeight = .5m;
            var currentRank = .1m;
            var expectedRank = 1;
            var idNumber = "123456";
            var person1 = _fixture.Build<Person>().With(x => x.IdentificationNumber, idNumber).Create();
            var person2 = _fixture.Build<Person>().With(x => x.IdentificationNumber, idNumber).Create();
            var ruleConfigDictionary = new Dictionary<PersonComparisonRuleType, RuleEngineConfiguration>
            {
                {PersonComparisonRuleType.IdentificationNumbersAreEqual,  new RuleEngineConfiguration(){ RuleType = PersonComparisonRuleType.IdentificationNumbersAreEqual, Weight=ruleWeight} }
            };

            var ruleInput = new PersonComparisonRuleInput(person1, person2, currentRank);
            var sut = new IdentificationNumbersAreEqualRule();

            var result = await sut.ExecuteAsync(ruleInput, ruleConfigDictionary);

            result.BreakCircuit.Should().BeTrue();
            result.NewRank.Should().Be(expectedRank);
        }

        [Fact]
        public async Task GivenTwoPersonWithDifferentIdNumber_WhenCompared_MustReturnNull()
        {
            var currentRank = .1m;
            var person1 = _fixture.Build<Person>().With(x => x.IdentificationNumber, "123456").Create();
            var person2 = _fixture.Build<Person>().With(x => x.IdentificationNumber, "000000").Create();
            var ruleConfigDictionary = new Dictionary<PersonComparisonRuleType, RuleEngineConfiguration>
            {
                {PersonComparisonRuleType.IdentificationNumbersAreEqual,  new RuleEngineConfiguration(){ RuleType = PersonComparisonRuleType.IdentificationNumbersAreEqual} }
            };

            var ruleInput = new PersonComparisonRuleInput(person1, person2, currentRank);
            var sut = new IdentificationNumbersAreEqualRule();

            var result = await sut.ExecuteAsync(ruleInput, ruleConfigDictionary);

            result.Should().BeNull();
        }
    }
}
