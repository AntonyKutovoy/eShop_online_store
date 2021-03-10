using Microsoft.AspNetCore.Http;
using Shop.DataAccess;
using Shop.DataAccess.Models;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Services
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public List<ProductViewModel> GetAllProducts()
        {
            var products = productRepository.GetAll();
            var productsViewModel = new List<ProductViewModel>();
            foreach (var product in products)
            {
                var productViewModel = product.ToProductViewModel();
                productsViewModel.Add(productViewModel);
            }
            return productsViewModel;
        }

        public ProductViewModel GetProduct (Guid id)
        {
            var product = productRepository.Get(id);
            var productViewModel = product.ToProductViewModel();
            return productViewModel;
        }
        public void Create(ProductViewModel productViewModel)
        {
            productRepository.Create(productViewModel.ToProduct());
        }
    }
}
