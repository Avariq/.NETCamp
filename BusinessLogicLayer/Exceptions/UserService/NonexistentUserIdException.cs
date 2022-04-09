using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnimeLib.Services.Exceptions.Root_exceptions;

namespace AnimeLib.Services.Exceptions
{
    public class NonexistentUserIdException : UserServiceException
    {
        public NonexistentUserIdException(int userId) : base($"User not found. UserId: {userId}", 404) { }        
    }
}
