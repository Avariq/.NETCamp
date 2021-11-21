using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeLib.API.Models
{
    public sealed class FilterBody
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string[] Values { get; set; }
        public string PropertyName { get; set; }
    }
}
