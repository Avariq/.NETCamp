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

        public string[] GetAllAnimeTitles()
        {
            var animes = context.Animes
                .ToArray();

            int size = animes.Length;
            string[] titles = new string[size];

            for (int i = 0; i < size; ++i)
            {
                titles[i] = animes[i].Title;
            }

            return titles;
        }

        public string[] GetArcTitlesByAnimeId(int id)
        {
            var arcs = context.Arcs
                .Where(a => a.AnimeId.Equals(id))
                .ToArray();

            int size = arcs.Length;
            string[] titles = new string[size];
            for (int i = 0; i < size; ++i)
            {
                titles[i] = arcs[i].Name;
            }

            return titles;
        }
        public int GetArcId(string arcName)
        {
            var arc = context.Arcs
                .Where(a => a.Name.Equals(arcName))
                .First();

            return arc.Id;
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

        public int GetEpisodeId(int arcId, string epName)
        {
            var ep = context.Episodes
                .Where(e => e.ArcId.Equals(arcId))
                .Where(e => e.Name.Equals(epName))
                .First();

            return ep.Id;
        }
    }
}



