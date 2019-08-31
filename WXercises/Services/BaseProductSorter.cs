
using System.Collections.Generic;
using WXercises.Models;

namespace WXercises.Services
{
    public abstract class BaseProductSorter : IProductSorter
    {
        public virtual List<Product> Sort(List<Product> products)
        {
            // Default is to not sort and return the products as is.
            return products;
        }
    }
}
