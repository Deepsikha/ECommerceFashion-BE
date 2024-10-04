using DataAccessLayer;
using ECommerceFashion.Data;
using ECommerceFashion.Interface;
using ECommerceFashion.ViewModels;
using Microsoft.EntityFrameworkCore;
using XAct;

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

        public async Task<List<CartDetailsVM>> GetCartDetails(int userId)
        {
            var data = await _dataContext.CartDetails
                .Join(_dataContext.ProductMaster, cd => cd.ProductId, p => p.Id, (cd, p) => new { cd, p })
                .Join(_dataContext.UserMaster, cdp => cdp.cd.UserId, u => u.Id, (cdp, u) => new { cdp, u })
                .Join(_dataContext.ImageGallery, cdpu => cdpu.cdp.p.ImageGalleryId, ig => ig.Id, (cdpu, ig) => new { cdpu, ig })
                .Where(s => s.cdpu.u.Id == userId)
                .Select(x => new CartDetailsVM
                {
                    Id = x.cdpu.cdp.cd.Id,
                    ProductId = x.cdpu.cdp.p.Id,
                    ProductName = x.cdpu.cdp.p.Name,
                    ProductPrice = x.cdpu.cdp.p.Price,
                    UserEmail = x.cdpu.u.EmailAddress,
                    Image = x.ig.FilePath,
                    Quantity = x.cdpu.cdp.cd.Quantity,
                    ProductTotalAmount = x.cdpu.cdp.cd.TotalAmount
                })
                .ToListAsync();
            return data;
        }

        public async Task<bool> GetCartDetailsAlreadyExist(int userId, int productId)
        {
            var data = await _dataContext.CartDetails.FirstOrDefaultAsync(s => s.UserId == userId && s.ProductId == productId);
            return data != null ? true : false;
        }

        public async Task<bool> GetWishListDetailsAlreadyExist(int userId, int productId)
        {
            var data = await _dataContext.WishlistDetails.FirstOrDefaultAsync(s => s.UserId == userId && s.ProductId == productId);
            return data != null ? true : false;
        }

        public async Task<CartDetails> GetCartDetailsByCartId(int cartId)
        {
            var data = await _dataContext.CartDetails.FirstOrDefaultAsync(s => s.Id == cartId);
            return data;
        }

        public async Task<bool> AddProductsToCart(CartDetails cartDetails)
        {
            await _dataContext.AddAsync(cartDetails);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProductsToCart(CartDetails cartDetails)
        {
            _dataContext.Update(cartDetails);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCartDetails(int cartId)
        {
            var data = await _dataContext.CartDetails.FirstOrDefaultAsync(s => s.Id == cartId);
            if (data != null)
            {
                _dataContext.Remove(data);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<WishlistVM>> GetWishlistDetails(int userId)
        {
            var data = await _dataContext.WishlistDetails
                .Join(_dataContext.ProductMaster, w => w.ProductId, p => p.Id, (w, p) => new { w, p })
                .Join(_dataContext.UserMaster, wp => wp.w.UserId, u => u.Id, (wp, u) => new { wp, u })
                 .Join(_dataContext.ImageGallery, cdpu => cdpu.wp.p.ImageGalleryId, ig => ig.Id, (cdpu, ig) => new { cdpu, ig })
                .Where(s => s.cdpu.u.Id == userId)
                .Select(x => new WishlistVM
                {
                    Id = x.cdpu.wp.w.Id,
                    ProductName = x.cdpu.wp.p.Name,
                    ProductPrice = x.cdpu.wp.p.Price,
                    ProductDescription = x.cdpu.wp.p.Description,
                    UserEmail = x.cdpu.u.EmailAddress,
                    Image = x.ig.FilePath
                })
                .ToListAsync();
            return data;
        }

        public async Task<bool> AddProductsToWishlist(WishlistDetails wishlistDetails)
        {
            await _dataContext.AddAsync(wishlistDetails);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteWishlistDetails(int id)
        {
            var data = await _dataContext.WishlistDetails.FirstOrDefaultAsync(s => s.Id == id);
            if (data != null)
            {
                _dataContext.Remove(data);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
