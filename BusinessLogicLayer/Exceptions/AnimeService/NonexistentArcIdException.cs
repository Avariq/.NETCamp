
using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class NonexistentArcIdException : AnimeServiceException
    {
        public NonexistentArcIdException(int arcId) : base($"Arc not found. Arc id: {arcId}", 404) { }
    }
}


