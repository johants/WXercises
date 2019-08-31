
using System.Collections.Generic;
using WXercises.Models;

namespace WXercises.Services
{
    /// <summary>
    /// Template Design Pattern
    /// </summary>
    public abstract class BaseProductSorter
    {
        public abstract List<Product> Sort(List<Product> products);
    }
}
