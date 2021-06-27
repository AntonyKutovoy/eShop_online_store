using Shop.DataAccess;
using Shop.Models;
using System;
using System.Collections.Generic;

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

        public void AddInformation(string userId, OrderViewModel orderViewModelInfo)
        {
            var orderInfo = orderViewModelInfo.ToOrderInfo();
            orderInfo.Id = orderRepository.TryGetByUserId(userId).Id;
            orderRepository.AddInformation(orderInfo);
        }

        public List<OrderViewModel> GetAllByUserId(string userId)
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

        public List<OrderViewModel> GetAll()
        {
            var allOrders = orderRepository.GetAll();
            var orderViewModels = new List<OrderViewModel>();
            if (allOrders != null)
            {
                foreach (var order in allOrders)
                {
                    orderViewModels.Add(order.ToOrderViewModel());
                }
            }
            return orderViewModels;
        }

        public OrderViewModel GetOrder(Guid id)
        {
            return orderRepository.TryGetByOrderId(id).ToOrderViewModel();
        }

        public void ChangeStatus(string status, Guid id)
        {
            var existingOrder = orderRepository.TryGetByOrderId(id);
            orderRepository.ChangeStatus(status, existingOrder.Id);
        }
    }
}