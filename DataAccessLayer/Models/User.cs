using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string Username { get; set; }
        [Required]
        [MaxLength(64)]
        [Column(TypeName = "char(64)")]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(64)]
        [Column(TypeName = "varchar(64)")]
        [EmailAddress]
        public string Email { get; set; }

        public int RoleId { get; set; }

        public virtual UserRole Role { get; set; }
    }
}
