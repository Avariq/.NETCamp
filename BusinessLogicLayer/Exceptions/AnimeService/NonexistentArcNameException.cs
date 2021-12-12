using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class NonexistentArcNameException : AnimeServiceException
    {
        public NonexistentArcNameException(string arcName, int animeId) : base($"Arc not found. Arc name: {arcName}, animeId: {animeId}", 404) { }
    }
}



