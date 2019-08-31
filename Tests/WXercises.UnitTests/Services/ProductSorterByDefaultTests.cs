using FluentAssertions;
using WXercises.Enums;
using WXercises.Services;
using Xunit;

namespace WXercises.UnitTests.Services
{
    public class ProductSorterByDefaultTests
    {
        private readonly ProductSorterByDefault _sut;

        public ProductSorterByDefaultTests()
        {
            // Arrange
            _sut = new ProductSorterByDefault();
        }

        [Fact]
        public void sort_product_should_return_products_not_sorted()
        {
            // Act
            var result = _sut.Sort(Mocks.MockData.GetProducts());

            // Assert
            result.Should().BeEquivalentTo(Mocks.MockData.GetProductSorted(SortOption.Default));
        }
    }
}
