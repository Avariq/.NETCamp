using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnimeLib.Domain.Models
{
    public class Anime
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Episodes { get; set; }
        [Required]
        public int Views { get; set; }
        [Required]
        [MaxLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string Description { get; set; }
        [Required]
        public int StatusId { get; set; }
        public Status Status { get; set; }
        [Required]
        public int AgeRestrictionId { get; set; }
        public AgeRestriction AgeRestriction { get; set; }
        public List<Arc> Arcs { get; set; } = new List<Arc>();
        public List<AnimeGenres> AnimeGenres { get; set; } = new List<AnimeGenres>();


        //prop for img
    }
}
