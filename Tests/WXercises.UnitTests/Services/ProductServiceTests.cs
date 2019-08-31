using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using WXercises.Enums;
using WXercises.Models;
using WXercises.Proxies;
using WXercises.Services;
using Xunit;

namespace WXercises.UnitTests.Services
{
    public class ProductServiceTests
    {
        private readonly IApiResourceProxy _apiResourceProxy;
        private readonly IProductSorterFactory _productSorterFactory;
        private readonly ProductsService _sut;

        public ProductServiceTests()
        {
            _apiResourceProxy = Substitute.For<IApiResourceProxy>();
            _productSorterFactory = new ProductSorterFactory(_apiResourceProxy);
            _sut = new ProductsService(_apiResourceProxy, _productSorterFactory);
        }

        [Theory]
        [InlineData(SortOption.Low)]
        [InlineData(SortOption.High)]
        [InlineData(SortOption.Ascending)]
        [InlineData(SortOption.Descending)]
        [InlineData(SortOption.Default)]
        [InlineData(SortOption.Recommended)]
        public async Task product_service_sort_should_return_correct_products(SortOption sortOption)
        {
            // Arrange
            _apiResourceProxy.GetProducts().Returns(Mocks.MockData.GetProducts());
            _apiResourceProxy.GetShopperHistory().Returns(Mocks.MockData.GetShopperHistory());
            
            // Act
            var result = await _sut.Sort(sortOption);

            // Assert
            result.Should().BeEquivalentTo(Mocks.MockData.GetProductSorted(sortOption));

            if (sortOption != SortOption.Recommended)
            {
                // Shopper history only called for recommended
                _apiResourceProxy.Received(0).GetShopperHistory();
            }
            else
            {
                _apiResourceProxy.Received(1).GetShopperHistory();
            }
        }

        [Theory]
        [InlineData(SortOption.Low)]
        [InlineData(SortOption.High)]
        [InlineData(SortOption.Ascending)]
        [InlineData(SortOption.Descending)]
        [InlineData(SortOption.Default)]
        [InlineData(SortOption.Recommended)]
        public async Task product_service_sort_throws_exception_when_getproducts_throws_an_exception(SortOption sortOption)
        {
            // Arrange
            _apiResourceProxy.GetProducts().Returns<List<Product>>(x => throw new HttpRequestException());

            // Act, Assert
            var result = await Assert.ThrowsAsync<HttpRequestException>( () => _sut.Sort(sortOption));
        }

        [Fact]
        public async Task product_service_sort_throws_exception_when_getshopperhistory_throws_an_exception()
        {
            // Arrange
            _apiResourceProxy.GetProducts().Returns(Mocks.MockData.GetProducts());
            _apiResourceProxy.GetShopperHistory().Returns<List<ShopperHistory>>(x => throw new HttpRequestException());

            // Act, Assert
            var result = await Assert.ThrowsAsync<HttpRequestException>(() => _sut.Sort(SortOption.Recommended));
        }

        [Fact]
        public async Task product_service_trolleytotal_throws_exception_when_apiresourceproxy_throws_an_exception()
        {
            // Arrange
            _apiResourceProxy.GetTrolleyCalculator(Arg.Any<TrolleyTotalRequest>()).Returns<Decimal>(x => throw new HttpRequestException());

            // Act, Assert
            var result = await Assert.ThrowsAsync<HttpRequestException>(() =>  _sut.TrolleyTotal(new TrolleyTotalRequest()));
        }
    }
}
