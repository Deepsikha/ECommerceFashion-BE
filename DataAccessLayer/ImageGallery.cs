using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
    public class ImageGallery
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public bool IsActive{ get; set; }
    }
}
