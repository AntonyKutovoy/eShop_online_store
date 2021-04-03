using Shop.DataAccess;
using Shop.DataAccess.Models;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Services
{
    public class OrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public void AddProductToOrder(string userId, ProductViewModel productViewModel, int amount)
        {
            var existingOrder = orderRepository.TryGetByUserId(userId);
            var product = productViewModel.ToProduct();
            if (existingOrder == null)
            {
                orderRepository.Create(userId, product, amount);
            }
            else
            {
                orderRepository.AddProduct(existingOrder.Id, product, amount);
            }
        }

        public void AddInformation(string userId, OrderViewModel orderViewModel)
        {
            var existingOrder = orderRepository.TryGetByUserId(userId);
            orderRepository.AddInformation(existingOrder.Id, orderViewModel.UserAddress, orderViewModel.UserPhone,
                orderViewModel.Status, orderViewModel.DateTime, orderViewModel.UserFirstName, orderViewModel.UserLastName, orderViewModel.UserEmail, orderViewModel.UserComment);
        }

        public List<OrderViewModel> GetAll(string userId)
        {
            var userOrders = orderRepository.TryGetAllByUserId(userId);
            var orderViewModels = new List<OrderViewModel>();
            if (userOrders != null)
            {
                foreach(var userOrder in userOrders)
                {
                    orderViewModels.Add(userOrder.ToOrderViewModel());
                }
            }
            return orderViewModels;
        }
    }
}