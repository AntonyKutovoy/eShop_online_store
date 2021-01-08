using System;
using System.Collections.Generic;

namespace Shop.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public List<CartItemViewModel> Items { get; set; }
    }
}
