using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeLib.API.Config.Auth
{
    public class JwtConfigurer : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;

        public JwtConfigurer(IJwtAuthenticationManager _jwtAuthenticationManager)
        {
            jwtAuthenticationManager = _jwtAuthenticationManager;
        }

        public void Configure(string token, JwtBearerOptions options)
        {
            options.IncludeErrorDetails = true;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = jwtAuthenticationManager.FetchPKey(),
                ValidateIssuer = true,
                ValidIssuer = "AnimeLib",
                ValidateAudience = true,
                ValidAudience = "AnimeLib",
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                CryptoProviderFactory = new CryptoProviderFactory()
                {
                    CacheSignatureProviders = false
                }
            };

        }

        public void Configure(JwtBearerOptions options) { throw new NotImplementedException(); }
    }
}
