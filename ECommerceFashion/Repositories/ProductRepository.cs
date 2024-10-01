using DataAccessLayer;
using ECommerceFashion.Data;
using ECommerceFashion.Interface;
using ECommerceFashion.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ECommerceFashion.Repositories
{
    public class ProductRepository : IProductsRepository
    {
        private readonly DataContext _dataContext;
        public ProductRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //public async Task<List<ProductMaster>> GetAllProducts()
        //{
        //    var data = await _dataContext.ProductMaster.ToListAsync();
        //    return data;
        //}
        public async Task<List<ProductDetailsVM>> GetAllProductsByCategory(List<int> subCategoryListProducts)
        {
            var data = await _dataContext.ProductMaster
                .Join(_dataContext.SubCategoryMaster, p => p.SubCategoryId, sc => sc.Id, (p, sc) => new { p, sc })
                .Join(_dataContext.CategoryMaster, psc => psc.sc.CategoryId, pc => pc.Id, (psc, pc) => new { psc, pc })
                .Join(_dataContext.ImageGallery, ppsc => ppsc.psc.p.ImageGalleryId, pg => pg.Id, (ppsc, pg) => new { ppsc, pg })
                .Where(x => subCategoryListProducts.Contains(x.ppsc.psc.p.SubCategoryId))
                .Select(d => new ProductDetailsVM
                {
                    Id = d.ppsc.psc.p.Id,
                    Name = d.ppsc.psc.p.Name,
                    Description = d.ppsc.psc.p.Description,
                    CategoryName = d.ppsc.pc.Name,
                    SubCategoryName = d.ppsc.psc.sc.Name,
                    Price = d.ppsc.psc.p.Price,
                    Image = d.pg.FilePath,
                    IsActive = d.ppsc.psc.p.IsActive,
                })
                .ToListAsync();
            return data;
        }
        public async Task<List<ProductDetailsVM>> GetAllProductsBySubCategory(int subCategoryListProducts)
        {
            var data = await _dataContext.ProductMaster
                .Join(_dataContext.SubCategoryMaster, p => p.SubCategoryId, sc => sc.Id, (p, sc) => new { p, sc })
                .Join(_dataContext.CategoryMaster, psc => psc.sc.CategoryId, pc => pc.Id, (psc, pc) => new { psc, pc })
                .Join(_dataContext.ImageGallery, ppsc => ppsc.psc.p.ImageGalleryId, pg => pg.Id, (ppsc, pg) => new { ppsc, pg })
                .Where(x => subCategoryListProducts == x.ppsc.psc.p.SubCategoryId)
                .Select(d => new ProductDetailsVM
                {
                    Id = d.ppsc.psc.p.Id,
                    Name = d.ppsc.psc.p.Name,
                    Description = d.ppsc.psc.p.Description,
                    CategoryName = d.ppsc.pc.Name,
                    SubCategoryName = d.ppsc.psc.sc.Name,
                    Price = d.ppsc.psc.p.Price,
                    Image = d.pg.FilePath,
                    IsActive = d.ppsc.psc.p.IsActive,
                })
                .ToListAsync();
            return data;
        }

        public async Task<List<ProductDetailsVM>> GetAllProducts()
        {
            var data = await _dataContext.ProductMaster
                .Join(_dataContext.SubCategoryMaster, p => p.SubCategoryId, sc => sc.Id, (p, sc) => new { p, sc })
                .Join(_dataContext.CategoryMaster, psc => psc.sc.CategoryId, pc => pc.Id, (psc, pc) => new { psc, pc })
                .Join(_dataContext.ImageGallery, ppsc => ppsc.psc.p.ImageGalleryId, pg => pg.Id, (ppsc, pg) => new { ppsc, pg })
               
                .Select(d => new ProductDetailsVM
                {
                    Id = d.ppsc.psc.p.Id,
                    Name = d.ppsc.psc.p.Name,
                    Description = d.ppsc.psc.p.Description,
                    CategoryName = d.ppsc.pc.Name,
                    SubCategoryName = d.ppsc.psc.sc.Name,
                    Price = d.ppsc.psc.p.Price,
                    Image = d.pg.FilePath,
                    IsActive = d.ppsc.psc.p.IsActive,
                })
                .ToListAsync();
            return data;
        } 
        public async Task<ProductDetailsVM> GetAllProductsByProductId(int id)
        {
            var data = await _dataContext.ProductMaster
                .Join(_dataContext.SubCategoryMaster, p => p.SubCategoryId, sc => sc.Id, (p, sc) => new { p, sc })
                .Join(_dataContext.CategoryMaster, psc => psc.sc.CategoryId, pc => pc.Id, (psc, pc) => new { psc, pc })
                .Join(_dataContext.ImageGallery, ppsc => ppsc.psc.p.ImageGalleryId, pg => pg.Id, (ppsc, pg) => new { ppsc, pg })
                .Where(x => id == x.ppsc.psc.p.Id)
                .Select(d => new ProductDetailsVM
                {
                    Id = d.ppsc.psc.p.Id,
                    Name = d.ppsc.psc.p.Name,
                    Description = d.ppsc.psc.p.Description,
                    CategoryName = d.ppsc.pc.Name,
                    SubCategoryName = d.ppsc.psc.sc.Name,
                    Price = d.ppsc.psc.p.Price,
                    Image = d.pg.FilePath,
                    IsActive = d.ppsc.psc.p.IsActive,
                })
                .FirstOrDefaultAsync();
            return data;
        }
    }
}
