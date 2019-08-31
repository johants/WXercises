using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using WXercises.Helpers;

namespace WXercises.Extensions
{
    public static class HttpClientExtensions
    {
        public static IHttpClientBuilder AddHttpClientWithCircuitBreaker<TInterface, TImplementation>(this IServiceCollection services, string apiPrefix, IConfiguration configuration)
            where TInterface : class where TImplementation : class, TInterface
        {
            var apiConfiguration = new ApiEndpointConfiguration(apiPrefix, configuration);
            return services.AddHttpClient<TInterface, TImplementation>(client =>
                {
                    client.BaseAddress = apiConfiguration.BaseAddress;
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.Timeout = apiConfiguration.Timeout;
                })
                .AddTransientHttpErrorPolicy(builder =>
                {
                    return builder.CircuitBreakerAsync(apiConfiguration.ExceptionAllowedBeforeBreaking, TimeSpan.FromMinutes(apiConfiguration.DurationOfBreakInMinutes));
                });
        }
    }
}
