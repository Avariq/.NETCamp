using AnimeLib.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;


namespace AnimeLib.API.Config.Auth
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly RsaSecurityKey _privateRsaKey;

        public JwtAuthenticationManager ()
        {
            string rsaPrivateKeyBase64 = System.IO.File.ReadAllText(@"D:\SSH-keys\AnimeLibKeys\private-key.txt");
            RSA rsa = RSA.Create();
            byte[] rawKey = Convert.FromBase64String(rsaPrivateKeyBase64);
            rsa.ImportRSAPrivateKey(rawKey, out _);
            _privateRsaKey = new RsaSecurityKey(rsa);
        }

        public string FetchToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "AnimeLib",
                Audience = "AnimeLib",
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.Role)
                }),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(_privateRsaKey, SecurityAlgorithms.RsaSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public RsaSecurityKey FetchPKey()
        {
            return _privateRsaKey;
        }
    }
}
