using System;

namespace Shop.DataAccess.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}
