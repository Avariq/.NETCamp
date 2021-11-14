using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeLib.API.Models
{
    public class FilterArgs
    {
        public int[] statusIds { get; set; }
        public int[] arIds { get; set; }
        public int from_year { get; set; }
        public int to_year { get; set; }
        public int[] genreIds { get; set; }
    }
}
