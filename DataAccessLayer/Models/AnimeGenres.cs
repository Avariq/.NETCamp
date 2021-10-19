using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AnimeLib.Domain.Models
{
    public class AnimeGenres
    {
        [Required]
        public int AnimeId { get; set; }
        public virtual Anime Anime { get; set; }
        [Required]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
