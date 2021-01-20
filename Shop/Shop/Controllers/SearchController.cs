using Microsoft.AspNetCore.Mvc;
using Shop.DataAccess;
using Shop.Models;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Controllers
{
    public class SearchController : Controller
    {
        private const int productsCountPerPage = 9;
        private readonly ProductService productService;
        private readonly CartService cartService;
        private Guid userId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e");//временная переменная для проверок

        public SearchController(ProductService productService, CartService cartService)
        {
            this.productService = productService;
            this.cartService = cartService;
        }
        public IActionResult Index(int page = 1)
        {
            //var productsOnCurrentPage = searchProducts.Skip((page - 1) * productsCountPerPage).Take(productsCountPerPage).ToList();
            //var countPages = searchProducts.Count / productsCountPerPage;
            //if (searchProducts.Count % productsCountPerPage > 0)
            //{
            //    countPages++;
            //}
            //ViewData["countPages"] = countPages;
            //ViewData["productInCartCount"] = cartService.GetCurrentCart(userId).AllAmount;
            //return View(productsOnCurrentPage);
            return View();
        }

        public IActionResult Search(string searchproduct)
        {
            var products = productService.GetAllProducts();
            var searchProducts = new List<ProductViewModel>();
            if (!String.IsNullOrEmpty(searchproduct))
            {
                foreach (var searchProduct in products)
                {
                    if (searchProduct.Name.Contains(searchproduct))
                    {
                        searchProducts.Add(searchProduct);
                        ViewData["test"] = searchProducts.Count();//переменная для проверки (удалить)
                    }
                }
            }
            return RedirectToAction("Index");
        }


    }
}