using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using FraudDetection.Contracts.Ports;
using FraudDetection.Contracts.Services;
using FraudDetection.Models;
using FraudDetection.Services;
using FraudDetection.Services.RuleEngine;
using FraudDetection.Services.RuleEngine.Ruleset;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FraudDetection.UnitTests
{
    public class FirstNameSimilarRuleTests
    {
        private readonly IFixture _fixture;
        private readonly INameSimilarityCheck _nameSimilarityCheck;

        public FirstNameSimilarRuleTests()
        {
            _fixture = new Fixture();
            _nameSimilarityCheck = new NameSimilarityCheck();
        }

        [Fact]
        public async Task GivenTwoPersonForComparison_WhenFirstNameIsInitialsOfTheOtherPerson_DesinatedWeightMustBeAddedToResult()
        {
            var ruleWeight = .5m;
            var currentRank = .1m;
            var expectedRank = ruleWeight + currentRank;
            var person1 = _fixture.Build<Person>().With(x => x.FirstName, "Jack").Create();
            var person2 = _fixture.Build<Person>().With(x => x.FirstName, "J.").Create();
            var ruleConfigDictionary = new Dictionary<PersonComparisonRuleType, RuleEngineConfiguration>
            {
                {PersonComparisonRuleType.FirstNamesAreSimilar,  new RuleEngineConfiguration(){ RuleType = PersonComparisonRuleType.FirstNamesAreSimilar, Weight=ruleWeight} }
            };

            var ruleInput = new PersonComparisonRuleInput(person1, person2, currentRank);
            var sut = new FirstNamesAreSimilarRule(_nameSimilarityCheck);

            var result = await sut.ExecuteAsync(ruleInput, ruleConfigDictionary);

            result.NewRank.Should().Be(expectedRank);
        }

        [Fact]
        public async Task GivenTwoPersonWithSameFirstNameButWithTypo_WhenCompare_DesinatedWeightMustBeAddedToResult()
        {
            var ruleWeight = .5m;
            var currentRank = .1m;
            var expectedRank = ruleWeight + currentRank;
            var person1 = _fixture.Build<Person>().With(x => x.FirstName, "Andrew").Create();
            var person2 = _fixture.Build<Person>().With(x => x.FirstName, "Andew").Create();
            var ruleConfigDictionary = new Dictionary<PersonComparisonRuleType, RuleEngineConfiguration>
            {
                {PersonComparisonRuleType.FirstNamesAreSimilar,  new RuleEngineConfiguration(){ RuleType = PersonComparisonRuleType.FirstNamesAreSimilar, Weight=ruleWeight} }
            };

            var ruleInput = new PersonComparisonRuleInput(person1, person2, currentRank);
            var sut = new FirstNamesAreSimilarRule(_nameSimilarityCheck);

            var result = await sut.ExecuteAsync(ruleInput, ruleConfigDictionary);

            result.NewRank.Should().Be(expectedRank);
        }

    }
}
