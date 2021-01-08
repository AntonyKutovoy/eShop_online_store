namespace Shop.DataAccess.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}
