using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
    public class WishlistDetails
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
