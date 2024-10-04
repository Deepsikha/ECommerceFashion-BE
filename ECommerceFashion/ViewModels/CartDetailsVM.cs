namespace ECommerceFashion.ViewModels
{
    public class CartDetailsVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string UserEmail { get; set; }
        public int Quantity { get; set; }
        public string Image{ get; set; }
        public int ProductTotalAmount { get; set; }
    }
}
