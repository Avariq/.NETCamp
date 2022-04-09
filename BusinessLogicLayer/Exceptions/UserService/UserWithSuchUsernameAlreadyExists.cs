using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class UserWithSuchUsernameAlreadyExistsException : UserServiceException
    {
        public UserWithSuchUsernameAlreadyExistsException(string username) : base($"User with such username already exists. " +
                                                                            $"Username: {username}", 400)
        { }
    }
}
