using DataAccessLayer;
using ECommerceFashion.ViewModels;

namespace ECommerceFashion.Interface
{
    public interface IProductService
    {
        Task<List<ProductDetailsVM>> GetAllProducts(int categoryFilter, int subCategoryFilter);
        Task<ProductDetailsVM> GetProductByProductId(int id);
        Task<List<ProductDetailsVM>> GetAllProducts();
    }
}
