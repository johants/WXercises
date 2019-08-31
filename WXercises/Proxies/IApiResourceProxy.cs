using System.Collections.Generic;
using System.Threading.Tasks;
using WXercises.Models;

namespace WXercises.Proxies
{
    public interface IApiResourceProxy
    {
        Task<List<Product>> GetProducts();

        Task<List<ShopperHistory>> GetShopperHistory();

        Task<decimal> GetTrolleyCalculator(TrolleyTotalRequest request);
    }
}
