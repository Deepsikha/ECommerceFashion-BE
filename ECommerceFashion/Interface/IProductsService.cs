using DataAccessLayer;
using ECommerceFashion.ViewModels;

namespace ECommerceFashion.Interface
{
    public interface IProductService
    {
        Task<List<ProductDetailsVM>> GetAllProducts(int categoryFilter, int subCategoryFilter);
        Task<ProductDetailsVM> GetProductByProductId(int id);
        Task<List<ProductDetailsVM>> GetAllProducts();
        Task<List<CartDetailsVM>> GetCartDetails(int userId);
        Task<int> AddProductsToCart(AddToCartVM cartDetails);
        Task<bool> UpdateCartDetails(int cartId, AddToCartVM cartDetails);
        Task<bool> DeleteCartDetails(int cartId);
        Task<List<WishlistVM>> GetWishlistDetails(int userId);
        Task<int> AddProductsToWishlist(WishlistDetails wishlistDetails);
        Task<bool> DeleteWishlistDetails(int id);
    }
}
