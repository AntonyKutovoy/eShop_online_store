using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class PagerViewModel
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public IEnumerable<int> Pages { get; set; }
        public List<ProductViewModel> ProductsOnCurrentPage { get; set; }
        public bool HasPreviousPage
        {
            get
            {
                return (CurrentPage > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return (CurrentPage != EndPage && CurrentPage < EndPage);
            }
        }
    }
}
