using AnimeLib.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace AnimeLib.API.Config.Auth
{
    public interface IJwtAuthenticationManager
    {
        string FetchToken(User user);
        RsaSecurityKey FetchPKey();
    }
}