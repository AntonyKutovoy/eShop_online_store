using Shop.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Shop.DataAccess
{
    public interface IOrderRepository
    {
        Order TryGetByUserId(string userId);
        List<Order> TryGetAllByUserId(string userId);
        List<Order> GetAll();
        void AddProduct(Guid orderId, Product product, int amount);
        void Create(string userId, Product product, int amount);
        void AddInformation(Order orderInfo);
        void ChangeStatus(string status, Guid orderId);
        Order TryGetByOrderId(Guid orderId);
    }
}