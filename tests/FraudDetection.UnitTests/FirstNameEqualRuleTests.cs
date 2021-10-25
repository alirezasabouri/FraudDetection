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
    public class FirstNameEqualRuleTests
    {
        private readonly IFixture _fixture;

        public FirstNameEqualRuleTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GivenTwoPersonWithIdenticalFirstName_WhenCompared_DesinatedWeightMustBeAddedToResult()
        {
            var ruleWeight = .5m;
            var currentRank = .1m;
            var expectedRank = ruleWeight + currentRank;
            var firstName = "test";
            var person1 = _fixture.Build<Person>().With(x => x.FirstName, firstName).Create();
            var person2 = _fixture.Build<Person>().With(x => x.FirstName, firstName).Create();
            var ruleConfigDictionary = new Dictionary<PersonComparisonRuleType, RuleEngineConfiguration>
            {
                {PersonComparisonRuleType.FirstNamesAreEqual,  new RuleEngineConfiguration(){ RuleType = PersonComparisonRuleType.FirstNamesAreEqual, Weight=ruleWeight} }
            };

            var ruleInput = new PersonComparisonRuleInput(person1, person2, currentRank);
            var sut = new FirstNamesAreEqualRule();

            var result = await sut.ExecuteAsync(ruleInput, ruleConfigDictionary);

            result.NewRank.Should().Be(expectedRank);
        }

        [Fact]
        public async Task GivenTwoPersonWithDifferentFirstName_WhenCompared_ResultMustBeNull()
        {
            var ruleWeight = .5m;
            var currentRank = .1m;
            var person1 = _fixture.Build<Person>().With(x => x.FirstName, "firstName1").Create();
            var person2 = _fixture.Build<Person>().With(x => x.FirstName, "firstName2").Create();
            var ruleConfigDictionary = new Dictionary<PersonComparisonRuleType, RuleEngineConfiguration>
            {
                {PersonComparisonRuleType.FirstNamesAreEqual,  new RuleEngineConfiguration(){ RuleType = PersonComparisonRuleType.FirstNamesAreEqual, Weight=ruleWeight} }
            };

            var ruleInput = new PersonComparisonRuleInput(person1, person2, currentRank);
            var sut = new FirstNamesAreEqualRule();

            var result = await sut.ExecuteAsync(ruleInput, ruleConfigDictionary);

            result.Should().BeNull();
        }

        [Fact]
        public async Task GivenTwoPersonWithIdenticalFirstNameButDifferentCasing_WhenCompared_DesinatedWeightMustBeAddedToResult()
        {
            var ruleWeight = .5m;
            var currentRank = .1m;
            var expectedRank = ruleWeight + currentRank;
            var firstName = "test";
            var person1 = _fixture.Build<Person>().With(x => x.FirstName, firstName.ToLower()).Create();
            var person2 = _fixture.Build<Person>().With(x => x.FirstName, firstName.ToUpper()).Create();
            var ruleConfigDictionary = new Dictionary<PersonComparisonRuleType, RuleEngineConfiguration>
            {
                {PersonComparisonRuleType.FirstNamesAreEqual,  new RuleEngineConfiguration(){ RuleType = PersonComparisonRuleType.FirstNamesAreEqual, Weight=ruleWeight} }
            };

            var ruleInput = new PersonComparisonRuleInput(person1, person2, currentRank);
            var sut = new FirstNamesAreEqualRule();

            var result = await sut.ExecuteAsync(ruleInput, ruleConfigDictionary);

            result.NewRank.Should().Be(expectedRank);
        }

    }
}
