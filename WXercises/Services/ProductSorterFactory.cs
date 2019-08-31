using WXercises.Enums;
using WXercises.Proxies;

namespace WXercises.Services
{
    public class ProductSorterFactory : IProductSorterFactory
    {
        private readonly IApiResourceProxy _apiResourceProxy;

        private IProductSorter _lowProductSorter;
        private IProductSorter _highProductSorter;
        private IProductSorter _ascendingProductSorter;
        private IProductSorter _descendingProductSorter;
        private IProductSorter _defaultProductSorter;
        private IProductSorter _recommendedProductSorter;

        public IProductSorter LowProductSorter => _lowProductSorter ?? (_lowProductSorter = new ProductSorterByLow());
        public IProductSorter HighProductSorter => _highProductSorter ?? (_highProductSorter = new ProductSorterByHigh());
        public IProductSorter AscendingProductSorter => _ascendingProductSorter ?? (_ascendingProductSorter = new ProductSorterByAscending());
        public IProductSorter DescendingProductSorter => _descendingProductSorter ?? (_descendingProductSorter = new ProductSorterByDescending());
        public IProductSorter RecommendedProductSorter => _recommendedProductSorter ?? (_recommendedProductSorter = new ProductSorterByRecommended(_apiResourceProxy));
        public IProductSorter DefaultProductSorter => _defaultProductSorter ?? (_defaultProductSorter = new ProductSorterByDefault());

        public ProductSorterFactory(IApiResourceProxy apiResourceProxy)
        {
            _apiResourceProxy = apiResourceProxy;
        }

        public IProductSorter GetProductSorter(SortOption sortOption)
        {
            switch (sortOption)
            {
                case SortOption.Low:
                    return LowProductSorter;
                case SortOption.High:
                    return HighProductSorter;
                case SortOption.Ascending:
                    return AscendingProductSorter;
                case SortOption.Descending:
                    return DescendingProductSorter;
                case SortOption.Recommended:
                    return RecommendedProductSorter;
                default:
                    return DefaultProductSorter;
            }
        }
    }
}
