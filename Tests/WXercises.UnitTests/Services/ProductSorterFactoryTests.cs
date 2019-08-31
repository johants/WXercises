using FluentAssertions;
using NSubstitute;
using WXercises.Enums;
using WXercises.Proxies;
using WXercises.Services;
using Xunit;

namespace WXercises.UnitTests.Services
{
    public class ProductSorterFactoryTests
    {
        private readonly IApiResourceProxy _apiResourceProxy;
        private readonly ProductSorterFactory _sut;

        public ProductSorterFactoryTests()
        {
            // Arrange
            _apiResourceProxy = Substitute.For<IApiResourceProxy>();
            _sut = new ProductSorterFactory(_apiResourceProxy);
        }

        [Theory]
        [InlineData(SortOption.Default, "ProductSorterByDefault")]
        [InlineData(SortOption.Low, "ProductSorterByLow")]
        [InlineData(SortOption.High, "ProductSorterByHigh")]
        [InlineData(SortOption.Ascending, "ProductSorterByAscending")]
        [InlineData(SortOption.Descending, "ProductSorterByDescending")]
        [InlineData(SortOption.Recommended, "ProductSorterByRecommended")]

        public void create_based_on_sort_option_should_return_correct_type(SortOption sortOption, string productSorterType)
        {
            // Act
            var result = _sut.GetProductSorter(sortOption);

            // Assert
            result.GetType().Name.Should().Be(productSorterType);
        }
    }
}
