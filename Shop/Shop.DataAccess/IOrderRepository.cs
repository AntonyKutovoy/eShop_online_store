using Shop.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Shop.DataAccess
{
    public interface IOrderRepository
    {
        Order TryGetByUserId(string userId);
        List<Order> TryGetAllByUserId(string userId);
        void AddProduct(Guid orderId, Product product, int amount);
        void Create(string userId, Product product, int amount);
        void AddInformation(Guid orderId, string address, string userPhone, string status,
            DateTime dateTime, string userFirstName, string userLastName, string userEmail, string userComment);
    }
}