using DataAccessLayer;
using ECommerceFashion.Interface;
using ECommerceFashion.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceFashion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetProducts(int categoryFilter, int subCategoryFilter)
        {
            try
            {
                var res = await _productService.GetAllProducts(categoryFilter, subCategoryFilter);
                return StatusCode(StatusCodes.Status200OK, new { message = "Retrived Products list successfully.", result = res });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retriving products.: " + ex);
            }
        }

        [HttpGet("GetProductsById")]
        public async Task<IActionResult> GetProductsById(int id)
        {
            try
            {
                var res = await _productService.GetProductByProductId(id);
                return StatusCode(StatusCodes.Status200OK, new { message = "Retrived Product successfully.", result = res });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retriving product.: " + ex);
            }
        }

        [HttpGet("GetAllProductsList")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var res = await _productService.GetAllProducts();
                return StatusCode(StatusCodes.Status200OK, new { message = "Retrived all Product successfully.", result = res });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retriving product.: " + ex);
            }
        }

        [HttpGet("GetUserCartDetails")]
        public async Task<IActionResult> GetCartDetails(int userId)
        {
            try
            {
                var res = await _productService.GetCartDetails(userId);
                return StatusCode(StatusCodes.Status200OK, new { message = "Retrived User Cart Details successfully.", result = res });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retriving cart details.: " + ex);
            }
        }

        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddProductsToCart([FromBody] AddToCartVM cartDetails)
        {
            try
            {
                var res = await _productService.AddProductsToCart(cartDetails);
                if (res == 1)
                    return StatusCode(StatusCodes.Status200OK, new { message = "Added User Cart Details successfully.", result = res });
                else if (res == -1)
                    return StatusCode(StatusCodes.Status200OK, new { message = "Already Added In Cart.", result = res });
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Error while adding data to the cart." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding cart details.: " + ex);
            }
        }

        [HttpPatch("UpdateCart")]
        public async Task<IActionResult> UpdateCartDetails(int cartId, [FromBody] AddToCartVM cartDetails)
        {
            try
            {
                var res = await _productService.UpdateCartDetails(cartId, cartDetails);
                if (res)
                    return StatusCode(StatusCodes.Status200OK, new { message = "Updated User Cart Details successfully.", result = res });
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Error while Updating data to the cart." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while updating cart details.: " + ex);
            }
        }

        [HttpDelete("DeleteCartItem")]
        public async Task<IActionResult> DeleteCartDetails(int cartId)
        {
            try
            {
                var res = await _productService.DeleteCartDetails(cartId);
                if (res)
                    return StatusCode(StatusCodes.Status200OK, new { message = "Deleted User Cart Details successfully.", result = res });
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Error while deleting data to the cart." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting cart details.: " + ex);
            }
        }

        [HttpGet("GetUserWishlistDetails")]
        public async Task<IActionResult> GetWishlistDetails(int userId)
        {
            try
            {
                var res = await _productService.GetWishlistDetails(userId);
                return StatusCode(StatusCodes.Status200OK, new { message = "Retrived User Wishlist Details successfully.", result = res });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retriving wishlist details.: " + ex);
            }
        }

        [HttpPost("AddToWishlist")]
        public async Task<IActionResult> AddProductsToWishlist([FromBody] WishlistDetails wishlistDetails)
        {
            try
            {
                var res = await _productService.AddProductsToWishlist(wishlistDetails);
                if (res == 1)
                    return StatusCode(StatusCodes.Status200OK, new { message = "Added User wishlist Details successfully.", result = res });
                else if (res == -1)
                    return StatusCode(StatusCodes.Status200OK, new { message = "Already added in wishlist.", result = res });
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Error while adding data to the wishlist." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while adding wishlist details.: " + ex);
            }
        }

        [HttpDelete("DeleteWishlistItem")]
        public async Task<IActionResult> DeleteWishlistDetails(int id)
        {
            try
            {
                var res = await _productService.DeleteWishlistDetails(id);
                if (res)
                    return StatusCode(StatusCodes.Status200OK, new { message = "Deleted User wishlist Details successfully.", result = res });
                else
                    return StatusCode(StatusCodes.Status400BadRequest, new { message = "Error while deleting data to the wishlist." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while deleting wishlist details.: " + ex);
            }
        }
    }
}
