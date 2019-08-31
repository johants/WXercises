using WXercises.Enums;
using WXercises.Proxies;

namespace WXercises.Services
{
    public class ProductSorterFactory : IProductSorterFactory
    {
        private readonly IApiResourceProxy _apiResourceProxy;

        private BaseProductSorter _lowProductSorter;
        private BaseProductSorter _highProductSorter;
        private BaseProductSorter _ascendingProductSorter;
        private BaseProductSorter _descendingProductSorter;
        private BaseProductSorter _defaultProductSorter;
        private BaseProductSorter _recommendedProductSorter;

        public BaseProductSorter LowProductSorter => _lowProductSorter ?? (_lowProductSorter = new ProductSorterByLow());
        public BaseProductSorter HighProductSorter => _highProductSorter ?? (_highProductSorter = new ProductSorterByHigh());
        public BaseProductSorter AscendingProductSorter => _ascendingProductSorter ?? (_ascendingProductSorter = new ProductSorterByAscending());
        public BaseProductSorter DescendingProductSorter => _descendingProductSorter ?? (_descendingProductSorter = new ProductSorterByDescending());
        public BaseProductSorter RecommendedProductSorter => _recommendedProductSorter ?? (_recommendedProductSorter = new ProductSorterByRecommended(_apiResourceProxy));
        public BaseProductSorter DefaultProductSorter => _defaultProductSorter ?? (_defaultProductSorter = new ProductSorterByDefault());

        public ProductSorterFactory(IApiResourceProxy apiResourceProxy)
        {
            _apiResourceProxy = apiResourceProxy;
        }

        public BaseProductSorter GetProductSorter(SortOption sortOption)
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
