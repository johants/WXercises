using FluentAssertions;
using WXercises.Enums;
using WXercises.Services;
using Xunit;

namespace WXercises.UnitTests.Services
{
    public class ProductSorterByDescendingTests
    {
        private readonly ProductSorterByDescending _sut;

        public ProductSorterByDescendingTests()
        {
            // Arrange
            _sut = new ProductSorterByDescending();
        }

        [Fact]
        public void sort_product_should_return_products_sorted_by_name_from_z_to_a()
        {
            // Act
            var result = _sut.Sort(Mocks.MockData.GetProducts());

            // Assert
            result.Should().BeEquivalentTo(Mocks.MockData.GetProductSorted(SortOption.Descending));
        }
    }
}
