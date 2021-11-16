using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeLib.API.Models
{
    public class PageArgs
    {
        private const int maxPageSize = 20;
        public int pageNumber { get; set; } = 1;
        private int _pageSize = 12;
        public int pageSize { get { return _pageSize; } set { _pageSize = (value > maxPageSize) ? maxPageSize : value; } }
    }
}
