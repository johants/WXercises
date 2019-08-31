using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Polly.CircuitBreaker;
using WXercises.Models;
using WXercises.Proxies;
using Xunit;

namespace WXercises.UnitTests.Proxies
{
    public class ApiResourceProxyTests : ApiProxyTestBase
    {
        private IApiResourceProxy _sut;

        public ApiResourceProxyTests()
        {
            // Arrange
            _sut = GetClient<IApiResourceProxy>("http://apihost.com/api/resource/");
        }

        [Fact]
        public async void get_products_should_call_the_correct_endpoint()
        {
            // Act
            await _sut.GetProducts();

            // Assert
            MockHttpMessageHandler.Request.RequestUri.PathAndQuery.Should()
                .Be($"/api/resource/products?token={Token}");
        }

        [Fact]
        public async void get_shopper_history_should_call_the_correct_endpoint()
        {
            // Act
            await _sut.GetShopperHistory();

            // Assert
            MockHttpMessageHandler.Request.RequestUri.PathAndQuery.Should()
                .Be($"/api/resource/shopperhistory?token={Token}");
        }

        [Fact]
        public async void get_trolley_calculator_should_call_the_correct_endpoint()
        {
            // Arrange
            MockHttpMessageHandler.Response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("2", new ASCIIEncoding(), "application/json")
            };

            // Act
            await _sut.GetTrolleyCalculator(new TrolleyTotalRequest());

            // Assert
            MockHttpMessageHandler.Request.RequestUri.PathAndQuery.Should()
                .Be($"/api/resource/trolleyCalculator?token={Token}");
        }

        [Theory]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.RequestTimeout)]
        [InlineData(HttpStatusCode.NotImplemented)]
        [InlineData(HttpStatusCode.BadGateway)]
        public void apiresourceproxy_should_throw_exception_if_get_products_error_response(HttpStatusCode errorStatusCode)
        {
            // Assert
            _sut = GetClient<IApiResourceProxy>("http://apihost.com/api/resource/", errorStatusCode);

            // Act
            Func<Task> action = async () => await _sut.GetProducts();

            // Assert
            action.Should().Throw<HttpRequestException>();

            if (errorStatusCode != HttpStatusCode.Unauthorized && errorStatusCode != HttpStatusCode.BadRequest)
            {
                action = async () => await _sut.GetProducts();
                action.Should().Throw<BrokenCircuitException>();
            }
        }

        [Theory]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.RequestTimeout)]
        [InlineData(HttpStatusCode.NotImplemented)]
        [InlineData(HttpStatusCode.BadGateway)]
        public void apiresourceproxy_should_throw_exception_if_get_shopper_history_error_response(HttpStatusCode errorStatusCode)
        {
            // Assert
            _sut = GetClient<IApiResourceProxy>("http://apihost.com/api/resource/", errorStatusCode);

            // Act
            Func<Task> action = async () => await _sut.GetShopperHistory();

            // Assert
            action.Should().Throw<HttpRequestException>();

            if (errorStatusCode != HttpStatusCode.Unauthorized && errorStatusCode != HttpStatusCode.BadRequest)
            {
                action = async () => await _sut.GetShopperHistory();
                action.Should().Throw<BrokenCircuitException>();
            }
        }

        [Theory]
        [InlineData(HttpStatusCode.InternalServerError)]
        [InlineData(HttpStatusCode.BadRequest)]
        [InlineData(HttpStatusCode.Unauthorized)]
        [InlineData(HttpStatusCode.ServiceUnavailable)]
        [InlineData(HttpStatusCode.RequestTimeout)]
        [InlineData(HttpStatusCode.NotImplemented)]
        [InlineData(HttpStatusCode.BadGateway)]
        public void apiresourceproxy_should_throw_exception_if_get_trolley_calculator_error_response(HttpStatusCode errorStatusCode)
        {
            // Assert
            _sut = GetClient<IApiResourceProxy>("http://apihost.com/api/resource/", errorStatusCode);

            // Act
            Func<Task> action = async () => await _sut.GetTrolleyCalculator(new TrolleyTotalRequest());

            // Assert
            action.Should().Throw<HttpRequestException>();

            if (errorStatusCode != HttpStatusCode.Unauthorized && errorStatusCode != HttpStatusCode.BadRequest)
            {
                action = async () => await _sut.GetShopperHistory();
                action.Should().Throw<BrokenCircuitException>();
            }
        }
    }
}
