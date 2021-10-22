using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using FraudDetection.Api.Controllers;
using FraudDetection.Api.Dto;
using FraudDetection.Contracts.Usecases;
using FraudDetection.Misc;
using FraudDetection.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace FraudDetection.Api.UnitTests
{
    public class PersonControllerTests
    {
        private readonly Mock<IPersonUsecase> _personUsecaseMock;
        private readonly Mock<ILogger> _loggerMock;
        private readonly IFixture _fixture;

        private readonly PersonController _controller;

        public PersonControllerTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _personUsecaseMock = _fixture.Freeze<Mock<IPersonUsecase>>();
            _loggerMock = _fixture.Freeze<Mock<ILogger>>();
            _controller = _fixture.Build<PersonController>().OmitAutoProperties().Create();
        }

        [Fact]
        public async Task GivenCreatePersonEndpoint_WhenCreateAValidPerson_MustResultInHttpOk()
        {
            _personUsecaseMock.Setup(o => o.CreateAsync(It.IsAny<Person>())).ReturnsAsync(Result.Success());
            var createDto = _fixture.Create<PersonCreateDto>();

            IActionResult result = await _controller.Create(createDto);

            var objectResult = result.Should().BeOfType<OkResult>().Which;
            objectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task GivenCreatePersonEndpoint_WhenModelValidationNotPass_MustReturnBadRequest()
        {
            _personUsecaseMock.Setup(o => o.CreateAsync(It.IsAny<Person>())).ReturnsAsync(Result.Success());
            var createDto = _fixture.Create<PersonCreateDto>();
            _controller.ModelState.AddModelError("inputfield", "some validation error");

            IActionResult result = await _controller.Create(createDto);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GivenCreatePersonEndpoint_WhenUsecaseFails_MustReturnServerError()
        {
            _personUsecaseMock.Setup(o => o.CreateAsync(It.IsAny<Person>())).ReturnsAsync(Result.Fail(new Exception()));
            var createDto = _fixture.Create<PersonCreateDto>();

            IActionResult result = await _controller.Create(createDto);

            var objectResult = result.Should().BeOfType<ObjectResult>().Which;
            objectResult.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task GivenCreatePersonEndpoint_WhenUsecaseFails_MustLogTheError()
        {
            var exception = _fixture.Create<Exception>();
            _personUsecaseMock.Setup(o => o.CreateAsync(It.IsAny<Person>())).ReturnsAsync(Result.Fail(exception));
            var createDto = _fixture.Create<PersonCreateDto>();

            IActionResult result = await _controller.Create(createDto);

            _loggerMock.Verify(o => o.Error(exception, It.IsAny<string>()));
        }
    }
}
