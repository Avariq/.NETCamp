using AnimeLib.Domain.DataAccess;
using AnimeLib.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services
{
    public class AnimeService
    {
        private readonly AnimeContext context;

        public AnimeService(AnimeContext _context)
        {
            context = _context;
        }

        public List<Anime> GetAllAnimes()
        {
            var animes = context.Animes
                .ToList();
            return animes;
        }

        public List<Anime> GetAnimesByTitle(string title_fragment)
        {
            var animes = context.Animes
                .Where(a => a.Title.Contains(title_fragment))
                .ToList();
            
            return animes;
        }
    }
}
