using ECommerceFashion.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceFashion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetCategoriesListAsync()
        {
            try
            {
                var res = await _categoryService.GetCategoriesListAsync();
                return StatusCode(StatusCodes.Status200OK, new { message = "Retrived CategoryList successfully.", result = res });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retriving list of categories.: " + ex);
            }
        }
        
        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int Id)
        {
            try
            {
                var res = await _categoryService.GetCategoriesById(Id);
                return StatusCode(StatusCodes.Status200OK, new { message = "Retrived Category successfully.", result = res });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retriving category.: " + ex);
            }
        }

        [HttpGet("GetAllSubCategories")]
        public async Task<IActionResult> GetSubCategoriesListAsync()
        {
            try
            {
                var res = await _categoryService.GetSubCategoriesListAsync();
                return StatusCode(StatusCodes.Status200OK, new { message = "Retrived SubCategoryList successfully.", result = res });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retriving list of sub categories.: " + ex);
            }
        }

        [HttpGet("GetSubCategoryById")]
        public async Task<IActionResult> GetSubCategoryById(int Id)
        {
            try
            {
                var res = await _categoryService.GetSubCategoriesById(Id);
                return StatusCode(StatusCodes.Status200OK, new { message = "Retrived Sub Category successfully.", result = res });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retriving subcategory.: " + ex);
            }
        }

        [HttpGet("GetSubCategoryByCategoryId")]
        public async Task<IActionResult> GetSubCategoryByCategoryId(int categoryId)
        {
            try
            {
                var res = await _categoryService.GetSubCategoryByCategoryId(categoryId);
                return StatusCode(StatusCodes.Status200OK, new { message = "Retrived Sub Category successfully.", result = res });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error while retriving subcategory.: " + ex);
            }
        }
    }
}
