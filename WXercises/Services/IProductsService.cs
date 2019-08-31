using System.Collections.Generic;
using System.Threading.Tasks;
using WXercises.Enums;
using WXercises.Models;

namespace WXercises.Services
{
    public interface IProductsService
    {
        Task<List<Product>> Sort(SortOption sortOption);
        Task<decimal> TrolleyTotal(TrolleyTotalRequest request);
    }
}
