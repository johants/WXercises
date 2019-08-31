using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using WXercises.Enums;
using WXercises.Services;
using Xunit;

namespace WXercises.UnitTests.Services
{
    public class ProductSorterByLowTests
    {
        private readonly ProductSorterByLow _sut;

        public ProductSorterByLowTests()
        {
            // Arrange
            _sut = new ProductSorterByLow();
        }

        [Fact]
        public void sort_product_should_return_products_sorted_by_price_from_low_to_high()
        {
            // Act
            var result = _sut.Sort(Mocks.MockData.GetProducts());

            // Assert
            result.Should().BeEquivalentTo(Mocks.MockData.GetProductSorted(SortOption.Low));
        }
    }
}
