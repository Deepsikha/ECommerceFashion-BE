using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
    public class CartDetails
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public int TotalAmount { get; set; }
    }
}
