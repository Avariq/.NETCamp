using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeLib.API.Models.Input
{
    public class UserCredentials
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
