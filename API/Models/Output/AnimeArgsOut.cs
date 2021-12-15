using AnimeLib.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeLib.API.Models.Output
{
    public class AnimeArgsOut
    {
        public Anime[] animes { get; set; }
        public int totalPagesAmount { get; set; }
        public int totalAnimesFound { get; set; }
    }
}
