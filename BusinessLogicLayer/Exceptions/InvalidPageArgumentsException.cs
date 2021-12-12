using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class InvalidPageArgumentsException : AnimeServiceException
    {
        public InvalidPageArgumentsException() : base("Invalid page arguments provided", 400) { }
    }
}
