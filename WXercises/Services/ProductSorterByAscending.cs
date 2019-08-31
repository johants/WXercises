using System.Collections.Generic;
using System.Linq;
using WXercises.Models;

namespace WXercises.Services
{
    public class ProductSorterByAscending : BaseProductSorter
    {
        public override List<Product> Sort(List<Product> products)
        {
            return products.OrderBy(p => p.Name).ToList();
        }
    }
}
