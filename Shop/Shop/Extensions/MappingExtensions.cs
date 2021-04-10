using Shop.DataAccess.Models;
using System.Collections.Generic;

namespace Shop.Models
{
    public static class MappingExtensions
    {

        public static CartViewModel ToCartViewModel(this Cart cart)
        {
            return new CartViewModel
            {
                Id = cart.Id,
                Items = cart.CartItems.ToCartItemsViewModel()
            };
        }

        public static List<CartItemViewModel> ToCartItemsViewModel(this List<CartItem> cartItems)
        {
            var items = new List<CartItemViewModel>();
            foreach (var cartItem in cartItems)
            {
                var item = cartItem.ToCartItemViewModel();
                items.Add(item);
            }
            return items;
        }

        public static CartItemViewModel ToCartItemViewModel(this CartItem cartItem)
        {
            return new CartItemViewModel
            {
                Id = cartItem.Id,
                Amount = cartItem.Amount,
                Product = cartItem.Product.ToProductViewModel()
            };
        }

        public static ProductViewModel ToProductViewModel(this Product product)
        {
            return new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImagePath = product.ImagePath
            };
        }

        public static Product ToProduct(this ProductViewModel productViewModel)
        {
            var path = productViewModel.ImagePath;
            if (productViewModel.File != null)
            {
                path = "/images/products/" + productViewModel.File.FileName;
            }
            return new Product()
            {
                Id = productViewModel.Id,
                Name = productViewModel.Name,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
                ImagePath = path
            };
        }

        public static List<OrderItemViewModel> ToOrderItemsViewModel(this List<OrderItem> orderItems)
        {
            var items = new List<OrderItemViewModel>();
            foreach (var orderItem in orderItems)
            {
                var item = orderItem.ToOrderItemViewModel();
                items.Add(item);
            }
            return items;
        }

        public static OrderItemViewModel ToOrderItemViewModel(this OrderItem orderItem)
        {
            return new OrderItemViewModel
            {
                Id = orderItem.Id,
                Amount = orderItem.Amount,
                Product = orderItem.Product.ToProductViewModel()
            };
        }

        public static OrderViewModel ToOrderViewModel(this Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                Number = order.Number,
                UserComment = order.UserComment,
                UserAddress = order.UserAddress,
                UserPhone = order.UserPhone,
                UserFirstName = order.UserFirstName,
                UserLastName = order.UserLastName,
                UserEmail = order.UserEmail,
                UserId = order.UserId,
                Status = order.Status,
                DateTime = order.DateTime,
                OrderItems = order.OrderItems.ToOrderItemsViewModel()
            };
        }

        public static Order ToOrderInfo(this OrderViewModel orderViewModelInfo)
        {
            return new Order
            {
                Number = orderViewModelInfo.Number,
                UserComment = orderViewModelInfo.UserComment,
                UserAddress = orderViewModelInfo.UserAddress,
                UserPhone = orderViewModelInfo.UserPhone,
                UserFirstName = orderViewModelInfo.UserFirstName,
                UserLastName = orderViewModelInfo.UserLastName,
                UserEmail = orderViewModelInfo.UserEmail,
                Status = orderViewModelInfo.Status,
                DateTime = orderViewModelInfo.DateTime
            };
        }
    }
}
