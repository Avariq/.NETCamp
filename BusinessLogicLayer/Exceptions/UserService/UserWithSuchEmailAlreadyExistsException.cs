using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class UserWithSuchEmailAlreadyExistsException : UserServiceException
    {
        public UserWithSuchEmailAlreadyExistsException(string email) :base($"User with such email already exists. " +
                                                                            $"Email: {email}", 400) { }
    }
}
