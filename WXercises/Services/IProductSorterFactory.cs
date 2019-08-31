using WXercises.Enums;

namespace WXercises.Services
{
    public interface IProductSorterFactory
    {
        IProductSorter GetProductSorter(SortOption sortOption);
    }
}
