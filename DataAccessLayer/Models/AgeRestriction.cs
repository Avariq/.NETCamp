using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnimeLib.Domain.Models
{
    public class AgeRestriction
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string RestrictionCode { get; set; }
        [Required]
        public int AgeRequired { get; set; }
    }
}
