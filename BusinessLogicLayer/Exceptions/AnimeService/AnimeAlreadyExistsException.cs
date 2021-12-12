using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class AnimeAlreadyExistsException : AnimeServiceException
    {
        public AnimeAlreadyExistsException(string animeTitle) : base($"Anime does already exist. Anime title: {animeTitle}", 400) { }
    }
}




