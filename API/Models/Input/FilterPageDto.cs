using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeLib.API.Models.Input
{
    public class FilterPageDto
    {
        public PageArgs PageArguments { get; set; }
        public FilterBody[] Filters { get; set; }
    }
}
