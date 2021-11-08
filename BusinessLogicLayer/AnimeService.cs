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
                .Include(rs => rs.AgeRestriction)
                .Include(a => a.AnimeGenres)
                .Include(arc => arc.Arcs)
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

        public int GetAnimeId(string title)
        {
            var anime = context.Animes
                .Where(a => a.Title.Equals(title))
                .First();
            return anime.Id;
        }

        public Arc GetArcById(int id)
        {
            Arc arc = context.Arcs
                .Where(a => a.Id.Equals(id))
                .First();
            return arc;
                
        }

        public Arc CreateArc(Arc arc)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Arcs.Add(arc);
                    context.SaveChanges();
                    transaction.Commit();
                    return arc;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e.InnerException;
                }
            }
        }
    }
}



