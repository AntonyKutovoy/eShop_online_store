using Microsoft.AspNetCore.Hosting;
using Shop.DataAccess;
using Shop.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Shop.Services
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;
        IWebHostEnvironment appEnvironment;

        public ProductService(IProductRepository productRepository, IWebHostEnvironment appEnvironment)
        {
            this.productRepository = productRepository;
            this.appEnvironment = appEnvironment;
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

        public ProductViewModel GetProduct(Guid id)
        {
            var product = productRepository.Get(id);
            var productViewModel = product.ToProductViewModel();
            return productViewModel;
        }
        public void Create(ProductViewModel productViewModel)
        {
            if (productViewModel.File != null)
            {
                string path = "/images/products/" + productViewModel.File.FileName;
                productViewModel.File.CopyTo(new FileStream(appEnvironment.WebRootPath + path, FileMode.Create));
            }
            productRepository.Create(productViewModel.ToProduct());
        }
    }
}
