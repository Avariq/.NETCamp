using AnimeLib.API.Models;
using AnimeLib.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AnimeLib.API
{
    public static class FilterManager
    {
        private static readonly Dictionary<string, Func<IQueryable<Anime>, FilterBody, IQueryable<Anime>>> filterMapper =
            new Dictionary<string, Func<IQueryable<Anime>, FilterBody, IQueryable<Anime>>>()
            {
                ["TitleContainsText"] = ApplyTitleContainsText,
                ["HasStatus"] = ApplyHasStatus,
                ["HasAgeRestriction"] = ApplyHasAgeRestriction,
                ["YearSpanFrom"] = ApplyYearSpanFrom,
                ["YearSpanTo"] = ApplyYearSpanTo,
                ["HasGenres"] = ApplyHasGenres,
                ["OrderAscending"] = ApplyOrderAscending,
                ["OrderDescending"] = ApplyOrderDescending,
            };

        public static IQueryable<Anime> Apply(this IQueryable<Anime> animes, FilterBody filter)
        {
            var filterHandler = filterMapper[filter.Name];
            var result = filterHandler(animes, filter);
            return result;
        }
        public static IQueryable<Anime> ApplyTitleContainsText(IQueryable<Anime> animeData, FilterBody filter)
        {
            string textFragment = filter.Values[0];
            return animeData.Where(a => a.Title.Contains(textFragment));
        }

        public static IQueryable<Anime> ApplyHasStatus(IQueryable<Anime> animeData, FilterBody filter)
        {
            int[] statusIds = Array.ConvertAll(filter.Values, int.Parse);
            return animeData.Where(a => statusIds.Contains(a.StatusId));
        }

        public static IQueryable<Anime> ApplyHasAgeRestriction(IQueryable<Anime> animeData, FilterBody filter)
        {
            int[] arIds = Array.ConvertAll(filter.Values, int.Parse);
            return animeData.Where(a => arIds.Contains(a.AgeRestrictionId));
        }

        public static IQueryable<Anime> ApplyYearSpanFrom(IQueryable<Anime> animeData, FilterBody filter)
        {
            int yearFrom = int.Parse(filter.Values[0]);
            return animeData.Where(a => a.Year >= yearFrom);
        }

        public static IQueryable<Anime> ApplyYearSpanTo(IQueryable<Anime> animeData, FilterBody filter)
        {
            int yearTo = int.Parse(filter.Values[0]);
            return animeData.Where(a => a.Year <= yearTo);
        }

        public static IQueryable<Anime> ApplyHasGenres(IQueryable<Anime> animeData, FilterBody filter)
        {
            int[] genreIds = Array.ConvertAll(filter.Values, int.Parse);

            foreach (int genreId in genreIds)
            {
                animeData = animeData.Where(a => a.Genres.Any(g => g.GenreId.Equals(genreId)));
            }

            return animeData;
        }

        public static IQueryable<Anime> ApplyOrderAscending(IQueryable<Anime> animeData, FilterBody filter)
        {
            string property = filter.PropertyName;

            switch (property)
            {
                case "Title":
                    animeData = animeData.OrderByDescending(a => a.Title);
                    break;
                case "Year":
                    animeData = animeData.OrderBy(a => a.Year);
                    break;
                case "Rating":
                    animeData = animeData.OrderBy(a => a.Rating);
                    break;
                case "Votes":
                    animeData = animeData.OrderBy(a => a.Votes);
                    break;
                case "Views":
                    animeData = animeData.OrderBy(a => a.Views);
                    break;
                default:
                    break;
            }

            return animeData;
        }

        public static IQueryable<Anime> ApplyOrderDescending(IQueryable<Anime> animeData, FilterBody filter)
        {
            string property = filter.PropertyName;

            switch (property)
            {
                case "Title":
                    animeData = animeData.OrderBy(a => a.Title);
                    break;
                case "Year":
                    animeData = animeData.OrderByDescending(a => a.Year);
                    break;
                case "Rating":
                    animeData = animeData.OrderByDescending(a => a.Rating);
                    break;
                case "Votes":
                    animeData = animeData.OrderByDescending(a => a.Votes);
                    break;
                case "Views":
                    animeData = animeData.OrderByDescending(a => a.Views);
                    break;
                default:
                    break;
            }

            return animeData;
        }
    }
}
