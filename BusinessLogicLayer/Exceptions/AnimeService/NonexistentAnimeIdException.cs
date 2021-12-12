using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class NonexistentAnimeIdException : AnimeServiceException
    {
        public NonexistentAnimeIdException(int animeId) : base($"Anime not found. Id: {animeId}", 404) { }
    }
}
