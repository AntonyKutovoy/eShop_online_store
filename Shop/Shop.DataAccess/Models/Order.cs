using System;
using System.Collections.Generic;

namespace Shop.DataAccess.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string UserComment { get; set; }
        public string UserAddress { get; set; }
        public string UserPhone { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public DateTime DateTime { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
