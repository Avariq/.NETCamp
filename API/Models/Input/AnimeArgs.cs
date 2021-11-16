using AnimeLib.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeLib.API.Models
{
    public class AnimeArgs
    {
        public AnimeDto Anime { get; set; }
        public Genre[] Genres { get; set; }
    }
}
