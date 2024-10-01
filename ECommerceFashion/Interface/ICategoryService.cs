using DataAccessLayer;
using ECommerceFashion.ViewModels;

namespace ECommerceFashion.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryMaster>> GetCategoriesListAsync();
        Task<CategoryMaster> GetCategoriesById(int Id);
        Task<List<SubCategoryVM>> GetSubCategoriesListAsync();
        Task<SubCategoryVM> GetSubCategoriesById(int Id);
        Task<List<SubCategoryVM>> GetSubCategoryByCategoryId(int categoryId);
    }
}
