using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private const int pageSize = 12;
        private const int maxPages = 5;
        private readonly ProductService productService;

        public HomeController(ProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index(int page)
        {
            var products = productService.GetAllProducts();
            var pager = CreatePagination(products, page);
            return View(pager);
        }

        private PagerViewModel CreatePagination(List<ProductViewModel> products, int currentPage)
        {
            var productsOnCurrentPage = products.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling((decimal)products.Count / (decimal)pageSize);
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }
            int startPage, endPage;
            if (totalPages <= maxPages)
            {
                startPage = 1;
                endPage = totalPages;
            }
            else
            {
                var maxPagesBeforeCurrentPage = (int)Math.Floor((decimal)maxPages / (decimal)2);
                var maxPagesAfterCurrentPage = (int)Math.Ceiling((decimal)maxPages / (decimal)2) - 1;
                if (currentPage <= maxPagesBeforeCurrentPage)
                {
                    startPage = 1;
                    endPage = maxPages;
                }
                else if (currentPage + maxPagesAfterCurrentPage >= totalPages)
                {
                    startPage = totalPages - maxPages + 1;
                    endPage = totalPages;
                }
                else
                {
                    startPage = currentPage - maxPagesBeforeCurrentPage;
                    endPage = currentPage + maxPagesAfterCurrentPage;
                }
            }
            var pages = Enumerable.Range(startPage, (endPage + 1) - startPage);
            return new PagerViewModel()
            {
                TotalItems = products.Count,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalPages = totalPages,
                StartPage = startPage,
                EndPage = endPage,
                Pages = pages,
                ProductsOnCurrentPage = productsOnCurrentPage
            };
        }

        public IActionResult Search(string name)
        {
            var products = productService.GetAllProducts();
            var searchProducts = new List<ProductViewModel>();
            if (!string.IsNullOrEmpty(name))
            {
                name = name.ToUpper();
                searchProducts = products.Where(x => x.Name.ToUpper().Contains(name)).ToList();
            }
            return View(searchProducts);
        }
    }
}
