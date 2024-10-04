using DataAccessLayer;
using ECommerceFashion.Interface;
using ECommerceFashion.ViewModels;

namespace ECommerceFashion.Services
{
    public class ProductsService : IProductService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductsService(IProductsRepository productsRepository, ICategoryRepository categoryRepository)
        {
            _productsRepository = productsRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<ProductDetailsVM>> GetAllProducts(int categoryFilter, int subCategoryFilter)
        {
            try
            {
                if (categoryFilter != 0)
                {
                    var subcategoryList = await _categoryRepository.GetSubCategoriesListByCategoryId(categoryFilter);
                    var subcatIdList = subcategoryList.Select(x => x.Id).ToList();
                    var categoryProducts = await _productsRepository.GetAllProductsByCategory(subcatIdList);
                    return categoryProducts;
                }

                if (subCategoryFilter != 0)
                {
                    var subcategoryProducts = await _productsRepository.GetAllProductsBySubCategory(subCategoryFilter);
                    return subcategoryProducts;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public async Task<ProductDetailsVM> GetProductByProductId(int id)
        {
            try
            {
                if (id == 0)
                {
                    return null;
                }
                var products = await _productsRepository.GetAllProductsByProductId(id);
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public async Task<List<ProductDetailsVM>> GetAllProducts()
        {
            try
            {
                var products = await _productsRepository.GetAllProducts();
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        public async Task<List<CartDetailsVM>> GetCartDetails(int userId)
        {
            try
            {
                var cart = await _productsRepository.GetCartDetails(userId);
                return cart;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public async Task<int> AddProductsToCart(AddToCartVM cartDetails)
        {
            try
            {
                if (cartDetails == null)
                    return 0;

                if (cartDetails.ProductId == 0) return 0;
                var getProduct = await _productsRepository.GetAllProductsByProductId(cartDetails.ProductId);
                if (getProduct == null)
                    return 0;

                var getCartDetail = await _productsRepository.GetCartDetailsAlreadyExist(cartDetails.UserId, cartDetails.ProductId);
                if (getCartDetail)
                    return -1;

                    CartDetails cart = new()
                {
                    ProductId = cartDetails.ProductId,
                    UserId = cartDetails.UserId,
                    Quantity = cartDetails.Quantity,
                    TotalAmount = cartDetails.Quantity * getProduct.Price,
                };
                var addCartItems = await _productsRepository.AddProductsToCart(cart);
                return addCartItems ? 1 : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        
       
        public async Task<bool> UpdateCartDetails(int cartId, AddToCartVM cartDetails)
        {
            try
            {
                if (cartDetails == null)
                    return false;
                if (cartId == 0)
                    return false;

                var getCartDetail = await _productsRepository.GetCartDetailsByCartId(cartId);
                if (getCartDetail == null)
                    return false;

                if (cartDetails.ProductId == 0) return false;

                var getProduct = await _productsRepository.GetAllProductsByProductId(cartDetails.ProductId);
                if (getProduct == null)
                    return false;

                CartDetails cart = new()
                {
                    Id = cartId,
                    ProductId = cartDetails.ProductId,
                    UserId = cartDetails.UserId,
                    Quantity = cartDetails.Quantity,
                    TotalAmount = cartDetails.Quantity * getProduct.Price,
                };
                var updateCartItems = await _productsRepository.UpdateProductsToCart(cart);
                return updateCartItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public async Task<bool> DeleteCartDetails(int cartId)
        {
            try
            {
                if (cartId == 0)
                    return false;

                var deletedCartItem = await _productsRepository.DeleteCartDetails(cartId);
                return deletedCartItem;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
        
        public async Task<List<WishlistVM>> GetWishlistDetails(int userId)
        {
            try
            {
                var wishlistDetails = await _productsRepository.GetWishlistDetails(userId);
                return wishlistDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
         public async Task<int> AddProductsToWishlist(WishlistDetails wishlistDetails)
        {
            try
            {
                if (wishlistDetails == null)
                    return 0;

                if (wishlistDetails.ProductId == 0) return 0;
                var getProduct = await _productsRepository.GetAllProductsByProductId(wishlistDetails.ProductId);
                if (getProduct == null)
                    return 0;

                var getCartDetail = await _productsRepository.GetCartDetailsAlreadyExist(wishlistDetails.UserId, wishlistDetails.ProductId);
                if (getCartDetail)
                    return -1;



                var addWishlistItems = await _productsRepository.AddProductsToWishlist(wishlistDetails);
                return addWishlistItems ? 1 : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }

        public async Task<bool> DeleteWishlistDetails(int id)
        {
            try
            {
                if (id == 0)
                    return false;

                var deletedWishlistItem = await _productsRepository.DeleteWishlistDetails(id);
                return deletedWishlistItem;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw ex;
            }
        }
    }
}
