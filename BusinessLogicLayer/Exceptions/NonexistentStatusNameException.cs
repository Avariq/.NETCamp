using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class NonexistentStatusNameException : AnimeServiceException
    {
        public NonexistentStatusNameException(string statusName) : base($"Status not found. Status name: {statusName}", 404) { }
    }
}
