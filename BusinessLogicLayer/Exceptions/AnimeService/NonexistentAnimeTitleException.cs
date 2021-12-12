using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class NonexistentAnimeTitleException : AnimeServiceException
    {
        public NonexistentAnimeTitleException(string animeTitle) : base($"Anime not found. Title: {animeTitle}", 404) { }
    }
}
