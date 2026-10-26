using System;
using System.Collections.Generic;
using System.Text;

namespace GloboTicket.Application.Common
{
    public class Pagination<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public Pagination(int pageNumber, int pageSize, IEnumerable<T> items, int totalCount)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Items = items;
            TotalCount = totalCount;
        }
    }
}
