using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using WXercises.Controllers;
using WXercises.Enums;
using WXercises.Models;
using WXercises.Services;
using WXercises.UnitTests.Mocks;
using Xunit;

namespace WXercises.UnitTests.Controllers
{
    public class AnswersControllerTests
    {
        private IProductsService _productsService;
        private IUserService _userService;
        private AnswersController _answersController;
        public AnswersControllerTests()
        {
            _productsService = Substitute.For<IProductsService>();
            _userService = Substitute.For<IUserService>();
            _answersController = new AnswersController(_productsService, _userService);
        }

        [Fact]
        public void GetUser_should_return_user()
        {
            // Arrange
            var user = new User("Johan Sugiarto", "94cd0001-3e70-44d3-a1d1-ad62ba9f5ff2");
            _userService.GetUser().Returns(user);

            // Act
            var result = _answersController.Get();

            // Assert
            result.Should().NotBeNull();

            var content = result.Value;
            content.Should().BeEquivalentTo(user);
        }

        [Fact]
        public async Task Sort_should_return_products()
        {
            // Arrange
            _productsService.Sort(Arg.Any<SortOption>()).Returns(Task.FromResult(Mocks.MockData.GetProducts()));

            // Act
            var result = await _answersController.SortProducts();

            // Assert
            result.Should().NotBeNull();

            var content = result.Value;
            content.Should().BeEquivalentTo(Mocks.MockData.GetProducts());
        }

        [Fact]
        public async Task TrolleyTotal_should_return_total()
        {
            // Arrange
            _productsService.TrolleyTotal(Arg.Any<TrolleyTotalRequest>()).Returns(Task.FromResult(Mocks.MockData.GetTrolleyTotal()));

            // Act
            var result = await _answersController.TrolleyTotal(new TrolleyTotalRequest());

            // Assert
            result.Should().NotBeNull();

            var content = result.Value;
            content.Should().Be(Mocks.MockData.GetTrolleyTotal());
        }
    }
}
