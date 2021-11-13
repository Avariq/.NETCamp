using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Domain.Models
{
    public class AnimeGenre
    {
        public int GenreId { get; set; }
        public int AnimeId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Anime Anime { get; set; }
    }
}
