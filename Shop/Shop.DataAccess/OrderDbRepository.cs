using Microsoft.EntityFrameworkCore;
using Shop.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.DataAccess
{
    public class OrderDbRepository : IOrderRepository
    {
        private readonly ShopContext shopContext;

        public OrderDbRepository(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public Order TryGetByUserId(string userId)
        {
            var orders = GetAll();
            foreach (var order in orders)
            {
                if (order.UserId == userId && order.Status == null)
                {
                    return order;
                }
            }
            return null;
        }

        public List<Order> TryGetAllByUserId(string userId)
        {
            var allOrders = GetAll();
            var orders = new List<Order>();
            foreach (var order in allOrders)
            {
                if (order.UserId == userId)
                {
                    orders.Add(order);
                }
            }
            return orders;
        }

        public List<Order> GetAll()
        {
            return shopContext.Orders.AsNoTracking().Include(o => o.OrderItems).ThenInclude(p => p.Product).ToList();
        }

        public void AddProduct(Guid orderId, Product product, int amount)
        {
            var order = shopContext.Orders.FirstOrDefault(x => x.Id == orderId);
            order.OrderItems.Add(new OrderItem { Order = order, Product = product, Amount = amount, Id = Guid.NewGuid() });
            shopContext.SaveChanges();
        }

        public void Create(string userId, Product product, int amount)
        {
            var order = new Order { UserId = userId, Number = GetAll().Count + 1 };
            shopContext.Orders.Add(order);
            order.OrderItems.Add(new OrderItem { Order = order, Product = product, Amount = amount, Id = Guid.NewGuid() });
            shopContext.SaveChanges();
        }

        public void AddInformation(Guid orderId, string address, string userPhone, string status, DateTime dateTime,
            string userFirstName, string userLastName, string userEmail, string userComment)
        {
            var order = shopContext.Orders.FirstOrDefault(x => x.Id == orderId);
            if (address != null)
                order.UserAddress = address;
            if (userPhone != null)
                order.UserPhone = userPhone;
            if (status != null)
                order.Status = status;
            if (dateTime != null)
                order.DateTime = dateTime;
            if (userFirstName != null)
                order.UserFirstName = userFirstName;
            if (userLastName != null)
                order.UserLastName = userLastName;
            if (userEmail != null)
                order.UserEmail = userEmail;
            if (userComment != null)
                order.UserComment = userComment;
            shopContext.SaveChanges();
        }
    }
}