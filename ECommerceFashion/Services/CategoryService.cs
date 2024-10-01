using DataAccessLayer;
using ECommerceFashion.Interface;
using ECommerceFashion.ViewModels;

namespace ECommerceFashion.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryMaster>> GetCategoriesListAsync()
        {
            try
            {
                return await _categoryRepository.GetCategoriesListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public async Task<CategoryMaster> GetCategoriesById(int Id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoriesById(Id);
                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public async Task<List<SubCategoryVM>> GetSubCategoriesListAsync()
        {
            try
            {
                return await _categoryRepository.GetSubCategoriesListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public async Task<SubCategoryVM> GetSubCategoriesById(int Id)
        {
            try
            {
                var subcategory = await _categoryRepository.GetSubCategoriesById(Id);
                return subcategory;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public async Task<List<SubCategoryVM>> GetSubCategoryByCategoryId(int categoryId)
        {
            try
            {
                var data = await _categoryRepository.GetSubCategoryByCategoryId(categoryId);
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}
