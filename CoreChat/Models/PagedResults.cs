using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreChat.Models
{
    public class PagedResults<T>
    {
        public IEnumerable<T> Data { get; set; }
        public Pagination Pagination { get; set; }
    }
}
