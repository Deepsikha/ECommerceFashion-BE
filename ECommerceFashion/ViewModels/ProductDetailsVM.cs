namespace ECommerceFashion.ViewModels
{
    public class ProductDetailsVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public string? Image { get; set; }
        public bool IsActive { get; set; }
    }
}
