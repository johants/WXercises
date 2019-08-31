using FluentAssertions;
using NSubstitute;
using WXercises.Enums;
using WXercises.Proxies;
using WXercises.Services;
using Xunit;

namespace WXercises.UnitTests.Services
{
    public class ProductSorterByRecommendedTests
    {
        private readonly IApiResourceProxy _apiResourceProxy;
        private readonly ProductSorterByRecommended _sut;

        public ProductSorterByRecommendedTests()
        {
            // Arrange
            _apiResourceProxy = Substitute.For<IApiResourceProxy>();
            _sut = new ProductSorterByRecommended(_apiResourceProxy);
        }

        [Fact]
        public void sort_product_should_return_products_sorted_by_product_popularity()
        {
            // Arrange
            _apiResourceProxy.GetShopperHistory().Returns(Mocks.MockData.GetShopperHistory());

            // Act
            var result = _sut.Sort(Mocks.MockData.GetProducts());

            // Assert
            result.Should().BeEquivalentTo(Mocks.MockData.GetProductSorted(SortOption.Recommended));
        }
    }
}
