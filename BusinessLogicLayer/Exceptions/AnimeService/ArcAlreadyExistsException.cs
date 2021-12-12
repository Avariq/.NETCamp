using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class ArcAlreadyExistsException : AnimeServiceException
    {
        public ArcAlreadyExistsException(string arcName, int animeId) : base($"Arc does already exist. Arc name: {arcName}, animeId: {animeId}", 400) { }
    }
}



