﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Domain.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(16)]
        [Column(TypeName = "varchar(16)")]
        public string Role { get; set; }
    }
}
