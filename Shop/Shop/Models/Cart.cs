using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public List<CartItem> CartItems { get; set; }

        public static Cart GetItems(IServiceProvider services)
        {
            throw new NotImplementedException();
        }

        public static void AddToCart(Product product, int amount)
        {
            throw new NotImplementedException();
        }
    }
}
