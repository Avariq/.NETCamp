using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnimeLib.Domain.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(35)]
        [Column(TypeName = "varchar(35)")]
        public string Name { get; set; }

        public virtual ICollection<Anime> Animes { get; set; }
    }
}
