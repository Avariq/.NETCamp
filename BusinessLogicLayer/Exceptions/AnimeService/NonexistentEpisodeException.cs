using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class NonexistentEpisodeException : AnimeServiceException
    {
        public NonexistentEpisodeException(int arcId, string epName) : base($"Episode not found. ArcId: {arcId}, EpisodeName: {epName}", 404) { }
    }
}


