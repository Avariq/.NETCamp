using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class NonexistentUserUsernameException : AnimeServiceException
    {
        public NonexistentUserUsernameException(string username) : base($"User not found. Username: {username}", 404) { }
    }
}
