using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using WXercises.Models;
using WXercises.Services;
using Xunit;

namespace WXercises.UnitTests.Services
{
    public class UserServiceTests
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _sut;

        public UserServiceTests()
        {
            _configuration = Substitute.For<IConfiguration>();
            _sut = new UserService(_configuration);
        }

        [Fact]
        public void get_user_should_return_values_from_config()
        {
            // Arrange
            _configuration["user:name"].Returns("Johan Sugiarto");
            _configuration["user:token"].Returns("94cd0001-3e70-44d3-a1d1-ad62ba9f5ff2");
            var expected = new User("Johan Sugiarto", "94cd0001-3e70-44d3-a1d1-ad62ba9f5ff2");

            // Act
            var result = _sut.GetUser();

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
