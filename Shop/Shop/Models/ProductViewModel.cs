using Microsoft.AspNetCore.Http;
using Shop.DataAccess.Models;
using System;

namespace Shop.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public IFormFile File { set; get; }
    }
}