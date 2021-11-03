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
        public float Rating { get; set; }
        [Required]
        public int Year { get; set; }
        public int Episodes { get; set; }
        [Required]
        public int Views { get; set; }
        [Required]
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Image { get; set; }
        [Required]
        public int Votes { get; set; } = 0;
        [Required]
        [MaxLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string Description { get; set; }
        public virtual Status Status { get; set; }
        public virtual AgeRestriction AgeRestriction { get; set; }
        public virtual ICollection<Arc> Arcs { get; set; }
        public virtual ICollection<Genre> AnimeGenres { get; set; }

    }
}
