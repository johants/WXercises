using System.Collections.Generic;
using WXercises.Models;

namespace WXercises.Services
{
    public interface IProductSorter
    {
        List<Product> Sort(List<Product> products);
    }
}
