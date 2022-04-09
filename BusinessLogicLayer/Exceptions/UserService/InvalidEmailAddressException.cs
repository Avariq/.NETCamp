using AnimeLib.Services.Exceptions.Root_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions
{
    public class InvalidEmailAddressException : UserServiceException
    {
        public InvalidEmailAddressException(string email) : base($"Provided email address is not valid. Email: {email}", 400) { }
    }
}
