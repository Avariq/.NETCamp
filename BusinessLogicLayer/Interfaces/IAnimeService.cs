using AnimeLib.Domain.Models;
using System.Linq;

namespace AnimeLib.Services
{
    public interface IAnimeService
    {
        Anime CreateAnime(Anime anime, Genre[] genres);
        Arc CreateArc(Arc arc);
        Episode CreateEpisode(Episode episode);
        int GetAgeRestrictionId(string ageRestrictionCode);
        IQueryable<Anime> GetAllAnimesQueryable();
        string[] GetAllAnimeTitles();
        int[] GetAllARIds();
        int[] GetAllGenreIds();
        Genre[] GetAllGenres();
        (IQueryable<T>, int, int) Paginate<T>(IQueryable<T> items, int pageSize, int pageNumber);
        int[] GetAllStatusIds();
        int GetAnimeAmount();
        Anime GetAnimeById(int id);
        int GetAnimeId(string title);
        Arc GetArcById(int id);
        int GetArcId(string arcName, int animeId);
        string[] GetArcTitlesByAnimeId(int id);
        Episode GetEpisodeById(int id);
        int GetEpisodeId(int arcId, string epName);
        (Anime[], int) GetRecent(int pageNumber, int pageSize);
        int GetStatusId(string statusName);
        Anime GetRandomAnime();
        void DeleteAnimeByTitle(string animeTitle);
    }
}