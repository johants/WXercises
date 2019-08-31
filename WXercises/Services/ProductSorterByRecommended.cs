using System.Collections.Generic;
using System.Linq;
using WXercises.Models;
using WXercises.Proxies;

namespace WXercises.Services
{
    public class ProductSorterByRecommended : BaseProductSorter
    {
        private readonly IApiResourceProxy _apiResourceProxy;
 
        public ProductSorterByRecommended(IApiResourceProxy apiResourceProxy)
        {
            _apiResourceProxy = apiResourceProxy;
        }

        public override List<Product> Sort(List<Product> products)
        {
            var productNames = products.Select(p => p.Name).ToHashSet();

            var shopperHistory = _apiResourceProxy.GetShopperHistory().GetAwaiter().GetResult();
            var popularProducts = ProductsByPopularity(shopperHistory);
            var popularProductNames = popularProducts.Select(p => p.Name).ToHashSet();

            var sortedProducts = new List<Product>();

            // populate sortedProducts based on the popular products
            foreach (var popularProduct in popularProducts)
            {
                if (!productNames.Contains(popularProduct.Name)) continue;

                popularProduct.Quantity = 0;
                sortedProducts.Add(popularProduct);
            }

            // populate sortedProducts based on the products that are not in popular products list
            var productsNotInPopularProductSet = products.Where(p => !popularProductNames.Contains(p.Name)).ToList();
            foreach (var notPopularProduct in productsNotInPopularProductSet)
            {
                sortedProducts.Add(notPopularProduct);
            }

            return sortedProducts;
        }

        /// <summary>
        /// Helper method to get a list of products ordered by popularity
        /// </summary>
        /// <param name="shopperHistories"></param>
        /// <returns></returns>
        private List<Product> ProductsByPopularity(List<ShopperHistory> shopperHistories)
        {
            var products = new List<Product>();
            var popularity = new Dictionary<string, Product>();

            foreach (var history in shopperHistories)
            {
                products.AddRange(history.Products);
            }

            foreach (var product in products)
            {
                if (popularity.ContainsKey(product.Name))
                {
                    popularity[product.Name].Quantity = popularity[product.Name].Quantity + product.Quantity;
                }
                else
                {
                    popularity.Add(product.Name, product);
                }
            }

            var sorted = popularity.Select(p => p.Value).OrderByDescending(p => p.Quantity).ToList();

            return sorted;
        }
    }
}
