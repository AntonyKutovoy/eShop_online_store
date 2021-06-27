using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class IndexViewModel
    {
        public IEnumerable<ProductViewModel> ProductViewModels { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
