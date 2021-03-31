using System.Collections.Generic;

namespace Shop.Models
{
    public class UserOrdersViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }
        public UserViewModel User { get; set; }
    }
}
