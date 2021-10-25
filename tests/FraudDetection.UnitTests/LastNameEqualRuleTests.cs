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
    public class LastNameEqualRuleTests
    {
        private readonly IFixture _fixture;

        public LastNameEqualRuleTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GivenTwoPersonWithIdenticalLastName_WhenCompared_DesinatedWeightMustBeAddedToResult()
        {
            var ruleWeight = .5m;
            var currentRank = .1m;
            var expectedRank = ruleWeight + currentRank;
            var lastName = "test";
            var person1 = _fixture.Build<Person>().With(x => x.LastName, lastName).Create();
            var person2 = _fixture.Build<Person>().With(x => x.LastName, lastName).Create();
            var ruleConfigDictionary = new Dictionary<PersonComparisonRuleType, RuleEngineConfiguration>
            {
                {PersonComparisonRuleType.LastNamesAreEqual,  new RuleEngineConfiguration(){ RuleType = PersonComparisonRuleType.LastNamesAreEqual, Weight=ruleWeight} }
            };

            var ruleInput = new PersonComparisonRuleInput(person1, person2, currentRank);
            var sut = new LastNamesAreEqualRule();

            var result = await sut.ExecuteAsync(ruleInput, ruleConfigDictionary);

            result.NewRank.Should().Be(expectedRank);
        }

        [Fact]
        public async Task GivenTwoPersonWithDifferentLastName_WhenCompared_ResultMustBeNull()
        {
            var ruleWeight = .5m;
            var currentRank = .1m;
            var person1 = _fixture.Build<Person>().With(x => x.LastName, "lastName1").Create();
            var person2 = _fixture.Build<Person>().With(x => x.LastName, "lastName2").Create();
            var ruleConfigDictionary = new Dictionary<PersonComparisonRuleType, RuleEngineConfiguration>
            {
                {PersonComparisonRuleType.LastNamesAreEqual,  new RuleEngineConfiguration(){ RuleType = PersonComparisonRuleType.LastNamesAreEqual, Weight=ruleWeight} }
            };

            var ruleInput = new PersonComparisonRuleInput(person1, person2, currentRank);
            var sut = new LastNamesAreEqualRule();

            var result = await sut.ExecuteAsync(ruleInput, ruleConfigDictionary);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GivenTwoPersonWithIdenticalLastNameButDifferentCasing_WhenCompared_DesinatedWeightMustBeAddedToResult()
        {
            var ruleWeight = .5m;
            var currentRank = .1m;
            var expectedRank = ruleWeight + currentRank;
            var lastName = "test";
            var person1 = _fixture.Build<Person>().With(x => x.LastName, lastName.ToLower()).Create();
            var person2 = _fixture.Build<Person>().With(x => x.LastName, lastName.ToUpper()).Create();
            var ruleConfigDictionary = new Dictionary<PersonComparisonRuleType, RuleEngineConfiguration>
            {
                {PersonComparisonRuleType.LastNamesAreEqual,  new RuleEngineConfiguration(){ RuleType = PersonComparisonRuleType.LastNamesAreEqual, Weight=ruleWeight} }
            };

            var ruleInput = new PersonComparisonRuleInput(person1, person2, currentRank);
            var sut = new LastNamesAreEqualRule();

            var result = await sut.ExecuteAsync(ruleInput, ruleConfigDictionary);

            result.NewRank.Should().Be(expectedRank);
        }

    }
}
