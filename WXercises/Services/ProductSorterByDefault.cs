using System.Collections.Generic;
using WXercises.Models;

namespace WXercises.Services
{
    public class ProductSorterByDefault : BaseProductSorter
    {
        public override List<Product> Sort(List<Product> products)
        {
            return products;
        }
    }
}
