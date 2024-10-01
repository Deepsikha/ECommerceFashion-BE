using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
    public class SubCategoryMaster
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
