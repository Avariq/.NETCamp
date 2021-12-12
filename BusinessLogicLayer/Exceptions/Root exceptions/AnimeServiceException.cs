using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeLib.Services.Exceptions.Root_exceptions
{
    public abstract class AnimeServiceException : Exception
    {
        public int StatusCode { get; set; }
        protected AnimeServiceException(string message, int statusCode) : base(message) 
        {
            StatusCode = statusCode;
        }
    }
}
