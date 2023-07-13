using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model
{
    public class Pager
    {
        public int TotalItems { set; get; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int StartPage { get; set; }
        public int EndPage { set; get; }
        public int TotalPages { get; set; }
        public Pager()
        {

        }
        public Pager(int totalItem, int page, int pageSize = 10)
        {
            TotalPages = (int)Math.Ceiling((Decimal)totalItem / (Decimal)pageSize);
            CurrentPage = page;
            PageSize = pageSize;
            TotalItems = totalItem;
            StartPage = CurrentPage - 5;
            EndPage = CurrentPage + 4;

            if (StartPage <= 0)
            {
                EndPage = EndPage - (StartPage - 1);
                StartPage = 1;
            }
            if (EndPage > TotalPages)
            {
                EndPage = TotalPages;
                if (EndPage > 10)
                {
                    StartPage = EndPage - 9;
                }
            }
        }
    }
}
