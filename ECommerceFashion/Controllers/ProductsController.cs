using ECommerceFashion.Interface;
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
    }
}
