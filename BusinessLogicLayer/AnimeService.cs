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
                .Include(a => a.Genres)
                .ThenInclude(a => a.Genre)
                .Include(arc => arc.Arcs)
                .ThenInclude(ep => ep.Episodes)
                .ToArray();
            return animes;
        }

        public Anime[] GetRecent(int pageNumber, int pageSize)
        {
            var animes = context.Animes
                .Include(s => s.Status)
                .Include(rs => rs.AgeRestriction)
                .Include(a => a.Genres)
                .ThenInclude(a => a.Genre)
                .Include(arc => arc.Arcs)
                .ThenInclude(ep => ep.Episodes)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(a => a.Id)
                .ToArray();

            return animes;
        }

        public List<Anime> GetAnimesByFilter(int[] statusIds, int[] arIds, int from_year, 
                                            int to_year, int[] genreIds, int pageNumber, int pageSize)
        {
            var animes = context.Animes
                .Include(s => s.Status)
                .Include(rs => rs.AgeRestriction)
                .Include(a => a.Genres)
                .ThenInclude(a => a.Genre)
                .Include(arc => arc.Arcs)
                .ThenInclude(ep => ep.Episodes)
                .Where(a => statusIds.Contains(a.StatusId))
                .Where(a => arIds.Contains(a.AgeRestrictionId))
                .Where(a => a.Year >= from_year && a.Year <= to_year)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (genreIds != null)
            {
                List<Anime> animesToReturn = new List<Anime>();

                foreach (var anime in animes)
                {
                    List<int> genres = new List<int>();
                    foreach (var genre in anime.Genres)
                    {
                        genres.Add(genre.GenreId);
                    }

                    if (!genreIds.Except(genres).Any())
                    {
                        animesToReturn.Add(anime);
                    }
                }

                return animesToReturn;
            }

            return animes;   
        }

        public Anime[] GetAnimesByTitle(string title_fragment)
        {
            var animes = context.Animes
                .Include(s => s.Status)
                .Include(restr => restr.AgeRestriction)
                .Include(g => g.Genres)
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
                .Include(a => a.Episodes)
                .First();

            return arc; 
        }

        public Anime GetAnimeById(int id)
        {
            var anime = context.Animes
                .Where(a => a.Id.Equals(id))
                .Include(s => s.Status)
                .Include(restr => restr.AgeRestriction)
                .Include(g => g.Genres)
                .Include(arc => arc.Arcs)
                .ThenInclude(ep => ep.Episodes)
                .First();

            return anime;
        }

        public Anime CreateAnime(Anime anime, Genre[] genres)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    /*Genre genre = context.Genres
                        .Where(g => g.Id.Equals(1)) З цією штукою заводиться, хоча вона ідентична до того. шо я з фронта передаю
                        .First();*/

                    anime.Genres = new List<AnimeGenre>();
                    for (int i = 0; i < genres.Length; ++i)
                    {
                        Genre genre = context.Genres
                            .Where(g => g.Id.Equals(genres[i].Id))
                            .First();

                        anime.Genres.Add(new AnimeGenre { Genre = genre, Anime = anime });
                    }

                    context.Animes.Add(anime);
                    context.SaveChanges();
                    transaction.Commit();

                    return anime;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e.InnerException;
                }
            }
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

        public Episode GetEpisodeById(int id)
        {
            Episode ep = context.Episodes
                .Where(e => e.Id.Equals(id))
                .First();

            return ep;
        }

        public int GetEpisodeId(int arcId, string epName)
        {
            var ep = context.Episodes
                .Where(e => e.ArcId.Equals(arcId))
                .Where(e => e.Name.Equals(epName))
                .FirstOrDefault();
            if (ep == null)
            {
                return -1;
            }
            return ep.Id;
        }

        public Episode CreateEpisode(Episode episode)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Episodes.Add(episode);
                    context.SaveChanges();
                    transaction.Commit();
                    return episode;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e.InnerException;
                }
            }
        }

        public int GetStatusId(string statusName)
        {
            var status = context.Statuses
                .Where(s => s.StatusName.Equals(statusName))
                .First();

            return status.Id;
        }

        public int GetAgeRestrictionId(string ageRestrictionCode)
        {
            var ar = context.AgeRestrictions
                .Where(a => a.RestrictionCode.Equals(ageRestrictionCode))
                .First();

            return ar.Id;
        }

        public int[] GetAllStatusIds()
        {
            var statuses = context.Statuses
                .ToArray();

            int[] ids = new int[statuses.Length];
            for (int i = 0; i < statuses.Length; i++)
            {
                ids[i] = statuses[i].Id;
            }

            return ids;
        }

        public int[] GetAllARIds()
        {
            var ars = context.AgeRestrictions
                .ToArray();

            int[] ids = new int[ars.Length];
            for (int i = 0; i < ars.Length; i++)
            {
                ids[i] = ars[i].Id;
            }

            return ids;
        }

        public Genre[] GetAllGenres()
        {
            var genres = context.Genres
                .ToArray();

            return genres;
        }

        public int[] GetAllGenreIds()
        {
            var genres = GetAllGenres();

            int[] ids = new int[genres.Length];
            for (int i = 0; i < genres.Length; i++)
            {
                ids[i] = genres[i].Id;
            }

            return ids;      
        }

    }
}



