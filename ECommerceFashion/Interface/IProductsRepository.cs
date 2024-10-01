using DataAccessLayer;
using ECommerceFashion.ViewModels;

namespace ECommerceFashion.Interface
{
    public interface IProductsRepository
    {
        Task<List<ProductDetailsVM>> GetAllProducts();
        Task<List<ProductDetailsVM>> GetAllProductsByCategory(List<int> subCategoryListProducts);
        Task<List<ProductDetailsVM>> GetAllProductsBySubCategory(int subCategoryListProducts);
        Task<ProductDetailsVM> GetAllProductsByProductId(int id);
    }
}
