using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using WXercises.Extensions;
using WXercises.Proxies;

namespace WXercises.UnitTests.Proxies
{
    public class ApiProxyTestBase
    {
        protected MockHttpMessageHandler MockHttpMessageHandler;
        private IConfiguration _configuration;
        protected IServiceProvider _services;

        protected const string Token = "94cd0001-3e70-44d3-a1d1-ad62ba9f5ff2";

        public ApiProxyTestBase()
        {
            MockDependencies();
        }

        private void MockDependencies()
        {
            MockHttpMessageHandler = new MockHttpMessageHandler();

            _configuration = Substitute.For<IConfiguration>();
            _configuration[$"{Constants.ProxyPrefix.ApiResourceProxy}:timeoutSeconds"].Returns("3");
            _configuration[$"{Constants.ProxyPrefix.ApiResourceProxy}:circuitBreaker:exceptionAllowedBeforeBreaking"].Returns("1");
            _configuration[$"{Constants.ProxyPrefix.ApiResourceProxy}:circuitBreaker:durationOfBreakInMinutes"].Returns("1");

            var serviceCollection = new ServiceCollection();
            
            serviceCollection.AddScoped(provider => _configuration);
            serviceCollection.AddSingleton<IApiResourceProxy, ApiResourceProxy>();

            serviceCollection.AddHttpClientWithCircuitBreaker<IApiResourceProxy, ApiResourceProxy>(Constants.ProxyPrefix.ApiResourceProxy, _configuration)
                .ConfigureHttpMessageHandlerBuilder(b => { b.PrimaryHandler = MockHttpMessageHandler; });

            _services = serviceCollection.BuildServiceProvider();
        }

        protected T GetClient<T>(string baseAddress, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            MockHttpMessageHandler.MockStatusCode = httpStatusCode;
            _configuration[$"{Constants.ProxyPrefix.ApiResourceProxy}:hostAddress"].Returns(baseAddress);
            _configuration[$"{Constants.ProxyPrefix.ApiResourceProxy}:basePath"].Returns("");
            _configuration[$"{Constants.ProxyPrefix.ApiResourceProxy}:token"].Returns(Token);
            return _services.GetRequiredService<T>();
        }
    }
}
