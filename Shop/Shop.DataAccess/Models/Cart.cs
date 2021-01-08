using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Models
{
    /// <summary>
    /// Корзина
    /// </summary>
    public class Cart
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
