using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
        public string UserId { get; set; }
        public string UserComment { get; set; }
        public string UserAddress { get; set; }
        public string UserPhone { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string Status { get; set; }
        public DateTime DateTime { get; set; }

        public decimal FullPrice => OrderItems.Sum(x => x.Price);
        public int AllAmount => OrderItems.Sum(x => x.Amount);

    }
}
