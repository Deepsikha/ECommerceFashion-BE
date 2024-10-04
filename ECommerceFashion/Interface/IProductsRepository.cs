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
        Task<bool> GetCartDetailsAlreadyExist(int userId, int productId);
        Task<bool> GetWishListDetailsAlreadyExist(int userId, int productId);
        Task<List<CartDetailsVM>> GetCartDetails(int userId);
        Task<CartDetails> GetCartDetailsByCartId(int cartId);
        Task<bool> AddProductsToCart(CartDetails cartDetails);
        Task<bool> UpdateProductsToCart(CartDetails cartDetails);
        Task<bool> DeleteCartDetails(int cartId);
        Task<List<WishlistVM>> GetWishlistDetails(int userId);
        Task<bool> AddProductsToWishlist(WishlistDetails wishlistDetails);
        Task<bool> DeleteWishlistDetails(int id);
    }
}
