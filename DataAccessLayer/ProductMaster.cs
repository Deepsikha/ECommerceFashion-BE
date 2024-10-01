using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
    public class ProductMaster
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description{ get; set; }
        public int Price{ get; set; }
        public int SubCategoryId{ get; set; }
        public int ImageGalleryId{ get; set; }
        public bool IsActive{ get; set; }
    }
}
