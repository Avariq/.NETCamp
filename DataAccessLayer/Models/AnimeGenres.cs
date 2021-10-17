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
        public Anime Anime { get; set; }
        [Required]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
