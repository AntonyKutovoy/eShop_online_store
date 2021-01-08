namespace Shop.Models
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public ProductViewModel Product { get; set; }
        public int Amount { get; set; }
    }
}
