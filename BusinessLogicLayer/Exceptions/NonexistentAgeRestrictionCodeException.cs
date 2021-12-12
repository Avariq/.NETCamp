using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class NonexistentAgeRestrictionCodeException : AnimeServiceException
    {
        public NonexistentAgeRestrictionCodeException(string arCode) : base($"Age restriction not found. Age restriction code: {arCode}", 404) { }
    }
}

