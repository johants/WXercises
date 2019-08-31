using FluentAssertions;
using WXercises.Enums;
using WXercises.Services;
using Xunit;

namespace WXercises.UnitTests.Services
{
    public class ProductSorterByAscendingTests
    {
        private readonly ProductSorterByAscending _sut;

        public ProductSorterByAscendingTests()
        {
            // Arrange
            _sut = new ProductSorterByAscending();
        }

        [Fact]
        public void sort_product_should_return_products_sorted_by_name_from_a_to_z()
        {
            // Act
            var result = _sut.Sort(Mocks.MockData.GetProducts());

            // Assert
            result.Should().BeEquivalentTo(Mocks.MockData.GetProductSorted(SortOption.Ascending));
        }
    }
}
