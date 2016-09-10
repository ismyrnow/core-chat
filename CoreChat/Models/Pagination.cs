using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreChat.Models
{
    public class Pagination
    {
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPrevPage { get; set; }
        public int Count { get; set; }
        public int Limit { get; set; }
    }
}
