using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using FraudDetection.Contracts.Ports;
using FraudDetection.Contracts.Services;
using FraudDetection.Models;
using FraudDetection.Services.RuleEngine;
using FraudDetection.UnitTests.Dummies;
using FraudDetection.Usecases;
using Moq;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FraudDetection.UnitTests
{
    public class DuplicatePersonUsecaseTests
    {
        private readonly Mock<IRuleEngineConfigurationsRepository> _ruleEngineConfigurationsRepositoryMock;
        private readonly Mock<IPersonComparisonRuleEngineFactory> _personComparisonRuleEngineFactoryMock;
        private readonly IFixture _fixture;

        public DuplicatePersonUsecaseTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _ruleEngineConfigurationsRepositoryMock = _fixture.Freeze<Mock<IRuleEngineConfigurationsRepository>>();
            _personComparisonRuleEngineFactoryMock = _fixture.Freeze<Mock<IPersonComparisonRuleEngineFactory>>();
        }

        [Fact]
        public async Task GivenOnlyOneRule_WhenNewRankIsSet_SimilarityRankMustEqualsWithAppliedRankByRule()
        {
            var expectedRank = .5m;
            var ruleset = new HashSet<IPersonComparisonRule>() {
                new DummyComparisonRule(new PersonComparisonRuleOutput { NewRank = expectedRank })
            };
            _personComparisonRuleEngineFactoryMock.Setup(x => x.GetRuleSet()).Returns(ruleset);
            var sut = new DuplicatePersonUsecase(_ruleEngineConfigurationsRepositoryMock.Object, _personComparisonRuleEngineFactoryMock.Object);
            var person1 = _fixture.Create<Person>();
            var person2 = _fixture.Create<Person>();

            var result = await sut.FindSimilarityRankAsync(person1, person2);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(expectedRank);
        }

        [Fact]
        public async Task GivenMultipleRules_WhenFirstRuleBreakCircuit_RemainingRulesShouldNotChangeOutcomeRank()
        {
            var expectedRank = .5m;
            var ruleset = new HashSet<IPersonComparisonRule>() {
                new DummyComparisonRule(new PersonComparisonRuleOutput { NewRank = expectedRank, BreakCircuit=true }),
                new DummyComparisonRule(new PersonComparisonRuleOutput { NewRank = 100 })
            };
            _personComparisonRuleEngineFactoryMock.Setup(x => x.GetRuleSet()).Returns(ruleset);
            var sut = new DuplicatePersonUsecase(_ruleEngineConfigurationsRepositoryMock.Object, _personComparisonRuleEngineFactoryMock.Object);
            var person1 = _fixture.Create<Person>();
            var person2 = _fixture.Create<Person>();

            var result = await sut.FindSimilarityRankAsync(person1, person2);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(expectedRank);
        }

        [Fact]
        public async Task GivenMultipleRules_WhenFirstRuleReturnNull_MustContiniueWithRemainingRules()
        {
            var expectedRank = .5m;
            var ruleset = new HashSet<IPersonComparisonRule>() {
                new DummyComparisonRule(null),
                new DummyComparisonRule(new PersonComparisonRuleOutput { NewRank = expectedRank })
            };
            _personComparisonRuleEngineFactoryMock.Setup(x => x.GetRuleSet()).Returns(ruleset);
            var sut = new DuplicatePersonUsecase(_ruleEngineConfigurationsRepositoryMock.Object, _personComparisonRuleEngineFactoryMock.Object);
            var person1 = _fixture.Create<Person>();
            var person2 = _fixture.Create<Person>();

            var result = await sut.FindSimilarityRankAsync(person1, person2);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(expectedRank);
        }
    }
}
