using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WXercises.Helpers;
using WXercises.Models;

namespace WXercises.Proxies
{
    public class ApiResourceProxy : IApiResourceProxy
    {
        private readonly HttpClient _httpClient;
        private readonly IApiEndpointConfiguration _apiEndpointConfiguration;

        public ApiResourceProxy(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiEndpointConfiguration = new ApiEndpointConfiguration(Constants.ProxyPrefix.ApiResourceProxy, configuration);
        }
        public Task<List<Product>> GetProducts()
        {
            return ExecuteRequest<List<Product>>(() => _httpClient.GetAsync($"products?token={_apiEndpointConfiguration.Token}"));
        }

        public Task<List<ShopperHistory>> GetShopperHistory()
        {
            return ExecuteRequest<List<ShopperHistory>>(() => _httpClient.GetAsync($"shopperhistory?token={_apiEndpointConfiguration.Token}"));
        }

        public Task<decimal> GetTrolleyCalculator(TrolleyTotalRequest request)
        {
            return ExecuteRequest<decimal>(() => _httpClient.PostAsJsonAsync($"trolleyCalculator?token={_apiEndpointConfiguration.Token}", request));
        }

        private async Task<T> ExecuteRequest<T>(Func<Task<HttpResponseMessage>> request)
        {
            using (var response = await request())
            {
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<T>();
            }
        }
    }
}
