using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Models
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();

        public decimal FullPrice => Items.Sum(x => x.Price);
        public int AllAmount => Items.Sum(x => x.Amount);

    }
}
