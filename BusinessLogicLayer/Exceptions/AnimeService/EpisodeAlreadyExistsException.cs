using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class EpisodeAlreadyExistsException : AnimeServiceException
    {
        public EpisodeAlreadyExistsException(string epName) : base($"Episode does already exist. Episode name: {epName}", 400) { }
    }
}




