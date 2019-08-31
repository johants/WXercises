using System.Collections.Generic;
using System.Linq;
using WXercises.Models;

namespace WXercises.Services
{
    public class ProductSorterByHigh : BaseProductSorter
    {
        public override List<Product> Sort(List<Product> products)
        {
            return products.OrderByDescending(p => p.Price).ToList();
        }
    }
}
