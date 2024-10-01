using DataAccessLayer;
using ECommerceFashion.Data;
using ECommerceFashion.Interface;
using ECommerceFashion.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ECommerceFashion.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dataContext;
        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<CategoryMaster>> GetCategoriesListAsync()
        {
            return await _dataContext.CategoryMaster.ToListAsync();
        }

        public async Task<CategoryMaster> GetCategoriesById(int id)
        {
            return await _dataContext.CategoryMaster.Where(c => c.Id == id).FirstOrDefaultAsync();

        }
        public async Task<List<SubCategoryVM>> GetSubCategoriesListAsync()
        {
            return await _dataContext.SubCategoryMaster
                .Join(_dataContext.CategoryMaster, sc => sc.CategoryId, s => s.Id, (sc, s) => new { sc, s })
                .Select(x => new SubCategoryVM
                {
                    Id = x.sc.Id,
                    Name = x.sc.Name,
                    CategoryName = x.s.Name,
                    CategoryIsActive = x.s.IsActive,
                    IsActive = x.sc.IsActive
                })
                .ToListAsync();
        }

        public async Task<SubCategoryVM> GetSubCategoriesById(int id)
        {
            return await _dataContext.SubCategoryMaster
                .Join(_dataContext.CategoryMaster, sc => sc.CategoryId, s => s.Id, (sc, s) => new { sc, s })
                .Where(c => c.sc.Id == id)
                .Select(x => new SubCategoryVM
                {
                    Id = x.sc.Id,
                    Name = x.sc.Name,
                    CategoryName = x.s.Name,
                    IsActive = x.sc.IsActive
                })
                .FirstOrDefaultAsync();
        }
        public async Task<List<SubCategoryVM>> GetSubCategoriesListByCategoryId(int categoryId)
        {
            var data = await _dataContext.SubCategoryMaster
                .Join(_dataContext.CategoryMaster, sc => sc.CategoryId, s => s.Id, (sc, s) => new { sc, s })
                .Where(c => c.s.Id == categoryId)
                .Select(x => new SubCategoryVM
                {
                    Id = x.sc.Id,
                    Name = x.sc.Name,
                    CategoryName = x.s.Name,
                    IsActive = x.sc.IsActive
                })
                .ToListAsync();
            return data;
        }
        public async Task<List<SubCategoryVM>> GetSubCategoryByCategoryId(int categoryId)
        {
            var data = await _dataContext.SubCategoryMaster
                .Join(_dataContext.CategoryMaster, sc => sc.CategoryId, c => c.Id, (sc, c) => new { sc, c })
                .Where(x => x.c.Id == categoryId)
                .Select(x => new SubCategoryVM
                {
                    Id = x.sc.Id,
                    Name = x.sc.Name,
                    CategoryName = x.c.Name,
                    IsActive = x.sc.IsActive,
                    CategoryIsActive = x.c.IsActive
                })
                .ToListAsync();
            return data;
        }
    }
}
