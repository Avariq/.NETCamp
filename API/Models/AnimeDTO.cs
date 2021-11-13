using AnimeLib.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeLib.API.Models
{
    public class AnimeDTO
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public int Episodes { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public int AgeRestrictionId { get; set; }
    }
}
