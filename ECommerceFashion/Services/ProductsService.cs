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
    }
}
