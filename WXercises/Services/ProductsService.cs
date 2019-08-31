using System.Collections.Generic;
using System.Threading.Tasks;
using WXercises.Enums;
using WXercises.Models;
using WXercises.Proxies;

namespace WXercises.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IApiResourceProxy _apiResourceProxy;
        private readonly IProductSorterFactory _productSorterFactory;

        public ProductsService(IApiResourceProxy apiResourceProxy, IProductSorterFactory productSorterFactory)
        {
            _apiResourceProxy = apiResourceProxy;
            _productSorterFactory = productSorterFactory;
        }

        public async Task<List<Product>> Sort(SortOption sortOption)
        {
            var products = await _apiResourceProxy.GetProducts();

            var productSorter = _productSorterFactory.GetProductSorter(sortOption);

            return productSorter.Sort(products);
        }

        public async Task<decimal> TrolleyTotal(TrolleyTotalRequest request)
        {
            return await _apiResourceProxy.GetTrolleyCalculator(request);
        }
    }
}
