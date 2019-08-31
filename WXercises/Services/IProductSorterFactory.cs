using WXercises.Enums;

namespace WXercises.Services
{
    public interface IProductSorterFactory
    {
        BaseProductSorter GetProductSorter(SortOption sortOption);
    }
}
