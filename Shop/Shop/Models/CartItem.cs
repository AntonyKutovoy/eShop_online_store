namespace Shop.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
        public int CartId { get; set; }
    }
}
