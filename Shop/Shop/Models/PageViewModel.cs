using System;

namespace Shop.Models
{
    public class PageViewModel
    {
        public int PagesForDisplayCount = 3;
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PageViewModel(int count, int pageNumber, int productsOnCurrentPage, int productsCountPerPage)
        {
            PageNumber = pageNumber;
            if (productsOnCurrentPage == productsCountPerPage)
            {
                TotalPages = (int)Math.Ceiling(count / (double)productsOnCurrentPage);
            }
            //else
            //{
            //    TotalPages = pageNumber - 1;
            //}
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
