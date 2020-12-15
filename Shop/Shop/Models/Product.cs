using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Product
    {
        [Key]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}