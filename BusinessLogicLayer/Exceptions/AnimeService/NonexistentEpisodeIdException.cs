using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class NonexistentEpisodeIdException : AnimeServiceException
    {
        public NonexistentEpisodeIdException(int epId) : base($"Episode not found. Id: {epId}", 404) { }
    }
}

