using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
    public class CategoryMaster
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
