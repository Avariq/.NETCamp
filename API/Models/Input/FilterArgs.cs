using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeLib.API.Models
{
    public sealed class FilterArgs : PageArgs
    {
        public int[] statusIds { get; set; }
        public int[] arIds { get; set; }
        public int from_year { get; set; }
        public int to_year { get; set; }
        public int[] genreIds { get; set; }
        public string titleFragment { get; set; }
        public int orderBy { get; set; }
        public bool isDescending { get; set; }
    }
}
