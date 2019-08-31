using FluentAssertions;
using WXercises.Enums;
using WXercises.Services;
using Xunit;

namespace WXercises.UnitTests.Services
{
    public class ProductSorterByHighTests
    {
        private readonly ProductSorterByHigh _sut;

        public ProductSorterByHighTests()
        {
            // Arrange
            _sut = new ProductSorterByHigh();
        }

        [Fact]
        public void sort_product_should_return_products_sorted_by_price_from_high_to_low()
        {
            // Act
            var result = _sut.Sort(Mocks.MockData.GetProducts());

            // Assert
            result.Should().BeEquivalentTo(Mocks.MockData.GetProductSorted(SortOption.High));
        }
    }
}
