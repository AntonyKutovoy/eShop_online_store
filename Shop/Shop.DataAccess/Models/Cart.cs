using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Models
{
    /// <summary>
    /// Корзина
    /// </summary>
    public class Cart
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
