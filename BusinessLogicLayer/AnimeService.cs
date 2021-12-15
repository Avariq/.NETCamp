using AnimeLib.Domain.DataAccess;
using AnimeLib.Domain.Models;
using AnimeLib.Services.Exceptions;
using AnimeLib.Services.Exceptions.Root_exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly AnimeContext context;

        public AnimeService(AnimeContext _context)
        {
            context = _context;
        }

        public (int, int, int) GetPaginationValues(int totalItemsAmount, int pageSize, int pageNumber)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                throw new InvalidPageArgumentsException();
            }

            int totalPages = (int)Math.Ceiling((double)totalItemsAmount / (double)pageSize);

            int toSkip = (pageNumber - 1) * pageSize;
            int toTake = pageSize;
            
            return (toSkip, toTake, totalPages);
        }

        public int GetAnimeAmount()
        {
            var amount = context.Animes
                .Count();

            return amount;
        }

        protected IQueryable<Anime> GetAnimesPaginated(int toTake, int toSkip)
        {
            var animes = context.Animes
                .Include(s => s.Status)
                .Include(rs => rs.AgeRestriction)
                .Include(a => a.Genres)
                .ThenInclude(a => a.Genre)
                .Include(arc => arc.Arcs)
                .ThenInclude(ep => ep.Episodes)
                .OrderByDescending(a => a.Id)
                .Skip(toSkip)
                .Take(toTake);

            return animes;
        }

        public (Anime[], int) GetRecent(int pageNumber, int pageSize)
        {
            int animeAmount = GetAnimeAmount();

            (int toSkip, int toTake, int totalPages) = GetPaginationValues(animeAmount, pageSize, pageNumber);

            var animes = GetAnimesPaginated(toTake, toSkip).ToArray();
            

            return (animes, totalPages);
        }

        public IQueryable<Anime> GetAllAnimesQueryable()
        {
            var animes = context.Animes
                .Include(s => s.Status)
                .Include(rs => rs.AgeRestriction)
                .Include(a => a.Genres)
                .ThenInclude(a => a.Genre)
                .Include(arc => arc.Arcs)
                .ThenInclude(ep => ep.Episodes);

            foreach (var anime in animes)
            {
                foreach (var genre in anime.Genres)
                {
                    genre.Genre.Animes = null;
                }
            }

            /*string.IsNullOrWhiteSpace(stringnem)*/
            /*int.HasValue*/

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

        public void DeleteAnimeByTitle(string animeTitle)
        {
            var animeToRemove = context.Animes
                .Include(a => a.Genres)
                .Include(a => a.Arcs)
                .ThenInclude(a => a.Episodes)
                .Where(a => a.Title.Equals(animeTitle))
                .SingleOrDefault();

            if (animeToRemove != null)
            {
                context.Remove(animeToRemove);
                context.SaveChanges();
            }    
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
        public int GetArcId(string arcName, int animeId)
        {
            var arc = context.Arcs
                .Where(a => a.Name.Equals(arcName))
                .Where(a => a.AnimeId.Equals(animeId))
                .SingleOrDefault();

            if (arc == null)
            {
                throw new NonexistentArcNameException(arcName, animeId);
            }

            return arc.Id;
        }
        public Arc GetArcById(int arcId)
        {
            Arc arc = context.Arcs
                .Where(a => a.Id.Equals(arcId))
                .Include(a => a.Episodes)
                .SingleOrDefault();

            if (arc == null)
            {
                throw new NonexistentArcIdException(arcId);
            }

            return arc;
        }

        public Anime GetAnimeById(int id)
        {
            var anime = context.Animes
                .Where(a => a.Id.Equals(id))
                .Include(s => s.Status)
                .Include(restr => restr.AgeRestriction)
                .Include(g => g.Genres)
                .ThenInclude(g => g.Genre)
                .Include(arc => arc.Arcs)
                .ThenInclude(ep => ep.Episodes)
                .SingleOrDefault();

            if (anime == null)
            {
                throw new NonexistentAnimeIdException(id);
            }

            return anime;
        }

        public Anime CreateAnime(Anime anime, Genre[] genres)
        {
            Anime _anime = context.Animes.
                Where(a => a.Title.Equals(anime.Title))
                .SingleOrDefault();

            if (_anime != null)
            {
                throw new AnimeAlreadyExistsException(anime.Title);
            }

            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
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
                catch (Exception)
                {
                    transaction.Rollback();
                    throw new AnimeCreationException();
                }
            }
        }
        public Arc CreateArc(Arc arc)
        {
            var existingArcs = GetArcTitlesByAnimeId(arc.AnimeId);

            if (existingArcs.Contains(arc.Name))
            {
                throw new ArcAlreadyExistsException(arc.Name, arc.AnimeId);
            }

            context.Arcs.Add(arc);
            context.SaveChanges();

            return arc;
        }

        public Episode GetEpisodeById(int epId)
        {
            Episode ep = context.Episodes
                .Where(e => e.Id.Equals(epId))
                .SingleOrDefault();

            if (ep == null)
            {
                throw new NonexistentEpisodeIdException(epId);
            }

            return ep;
        }

        public int GetEpisodeId(int arcId, string epName)
        {
            var ep = context.Episodes
                .Where(e => e.ArcId.Equals(arcId))
                .Where(e => e.Name.Equals(epName))
                .SingleOrDefault();

            if (ep == null)
            {
                throw new NonexistentEpisodeException(arcId, epName);
            }

            return ep.Id;
        }

        public string[] GetEpisodeTitlesByArcId(int arcId)
        {
            Arc arc = GetArcById(arcId);
            if (arc == null)
            {
                throw new NonexistentArcIdException(arcId);
            }
            
            var episodes = context.Episodes
                .Where(ep => ep.ArcId.Equals(arcId))
                .ToArray();

            int size = episodes.Length;
            string[] episodeTitles = new string[size];

            for (int i = 0; i < size; i++)
            {
                episodeTitles[i] = episodes[i].Name;
            }

            return episodeTitles;
        }

        public Episode CreateEpisode(Episode episode)
        {
            var existingEpisodes = GetEpisodeTitlesByArcId(episode.ArcId);

            if (existingEpisodes.Contains(episode.Name))
            {
                throw new EpisodeAlreadyExistsException(episode.Name);
            }

            context.Episodes.Add(episode);
            context.SaveChanges();
            return episode;
        }

        public int GetStatusId(string statusName)
        {
            var status = context.Statuses
                .Where(s => s.StatusName.Equals(statusName))
                .SingleOrDefault();

            if (status == null)
            {
                throw new NonexistentStatusNameException(statusName);
            }

            return status.Id;
        }

        public int GetAgeRestrictionId(string ageRestrictionCode)
        {
            var ar = context.AgeRestrictions
                .Where(a => a.RestrictionCode.Equals(ageRestrictionCode))
                .SingleOrDefault();

            if (ar == null)
            {
                throw new NonexistentAgeRestrictionCodeException(ageRestrictionCode);
            }

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

        public Anime GetRandomAnime()
        {
            Random random = new Random();
            int animeAmount = GetAnimeAmount();
            int toSkip = random.Next(0, animeAmount);

            var anime = context.Animes
                .Skip(toSkip)
                .Take(1)
                .SingleOrDefault();

            return anime;
        }

    }
}