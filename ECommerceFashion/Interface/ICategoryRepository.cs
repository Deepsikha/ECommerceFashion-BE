using DataAccessLayer;
using ECommerceFashion.ViewModels;

namespace ECommerceFashion.Interface
{
    public interface ICategoryRepository
    {
        Task<List<CategoryMaster>> GetCategoriesListAsync();
        Task<CategoryMaster> GetCategoriesById(int id);
        Task<List<SubCategoryVM>> GetSubCategoriesListAsync();
        Task<List<SubCategoryVM>> GetSubCategoriesListByCategoryId(int categoryId);
        Task<SubCategoryVM> GetSubCategoriesById(int id);
        Task<List<SubCategoryVM>> GetSubCategoryByCategoryId(int categoryId);
    }
}
