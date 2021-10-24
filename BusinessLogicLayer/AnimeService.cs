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

        public Anime[] GetAllAnimes()
        {
            var animes = context.Animes
                .Include(s => s.Status)
                .Include(restr => restr.AgeRestriction)
                .Include(arcs => arcs.Arcs)
                .Include(g => g.AnimeGenres)
                .ToArray();
            return animes;
        }

        public Anime[] GetAnimesByTitle(string title_fragment)
        {
            var animes = context.Animes
                .Include(s => s.Status)
                .Include(restr => restr.AgeRestriction)
                .Include(g => g.AnimeGenres)
                .Include(arc => arc.Arcs)
                .ThenInclude(ep => ep.Episodes)
                .Where(a => a.Title.Contains(title_fragment))
                .ToArray();
            return animes;
        }
    }
}
