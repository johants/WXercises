using System;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using WXercises.Helpers;
using Xunit;

namespace WXercises.UnitTests.Helpers
{
    public class ApiEndPointConfigurationTests
    {
        private readonly IConfiguration _configuration;
        private readonly ApiEndpointConfiguration _sut;

        private const string Hostaddress = "http://www.example.com";
        private const string BasePath = "/example";
        private const string Token = "94cd0001-3e70-44d3-a1d1-ad62ba9f5ff2";
        private const string TimeoutSeconds = "5";
        private const string CircuitBreakerExceptionAllowedBeforeBreaking = "3";
        private const string CircuitBreakerDurationOfBreakInMinutes = "1";

        public ApiEndPointConfigurationTests()
        {
            // Arrange
            _configuration = Substitute.For<IConfiguration>();
            _sut = new ApiEndpointConfiguration(Constants.ProxyPrefix.ApiResourceProxy, _configuration);

            _configuration[$"{Constants.ProxyPrefix.ApiResourceProxy}:hostAddress"].Returns(Hostaddress);
            _configuration[$"{Constants.ProxyPrefix.ApiResourceProxy}:basePath"].Returns(BasePath);
            _configuration[$"{Constants.ProxyPrefix.ApiResourceProxy}:token"].Returns(Token);
            _configuration[$"{Constants.ProxyPrefix.ApiResourceProxy}:timeoutSeconds"].Returns(TimeoutSeconds);
            _configuration[$"{Constants.ProxyPrefix.ApiResourceProxy}:circuitBreaker:exceptionAllowedBeforeBreaking"].Returns(CircuitBreakerExceptionAllowedBeforeBreaking);
            _configuration[$"{Constants.ProxyPrefix.ApiResourceProxy}:circuitBreaker:durationOfBreakInMinutes"].Returns(CircuitBreakerDurationOfBreakInMinutes);
        }

        [Fact]
        public void ApiEndPointConfiguration_should_return_correct_settings()
        {
            // Act, Assert
            _sut.BaseAddress.Should().BeEquivalentTo(new Uri(new Uri(Hostaddress), BasePath));
            _sut.Token.Should().Be(Token);
            _sut.Timeout.Should().Be(TimeSpan.FromSeconds(double.Parse(TimeoutSeconds)));
            _sut.ExceptionAllowedBeforeBreaking.Should().Be(int.Parse(CircuitBreakerExceptionAllowedBeforeBreaking));
            _sut.DurationOfBreakInMinutes.Should().Be(int.Parse(CircuitBreakerDurationOfBreakInMinutes));
        }

    }
}
