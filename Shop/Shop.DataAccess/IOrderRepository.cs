using Shop.DataAccess.Models;
using System;

namespace Shop.DataAccess
{
    public interface IOrderRepository
    {
        Order TryGetByUserId(string userId);
        void AddProduct(Guid orderId, Product product, int amount);
        void Create(string userId, Product product, int amount);
        void AddInformation(Guid orderId, string address, string userPhone, string status,
            DateTime dateTime, string userFirstName, string userLastName, string userEmail);
    }
}