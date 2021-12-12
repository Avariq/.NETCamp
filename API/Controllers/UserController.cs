using AnimeLib.API.Config.Auth;
using AnimeLib.API.Models.Input;
using AnimeLib.Domain.Models;
using AnimeLib.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AnimeLib.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly UserService userService;
        private readonly IMapper mapper;
        private readonly IJwtAuthenticationManager jwtAuthManager;

        public UserController(ILogger<UserController> _logger, UserService _userService, IMapper _mapper, IJwtAuthenticationManager _jwtAuthManager)
        {
            logger = _logger;
            userService = _userService;
            mapper = _mapper;
            jwtAuthManager = _jwtAuthManager;
        }

        [HttpPost(nameof(Login))]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserCredentials userCredentials)
        {
            User currentUser = userService.GetUserByUsername(userCredentials.Username);
            if (currentUser is null)
            {
                return NotFound("User with such username does not exist");
            }

            if (!userCredentials.PasswordHash.Equals(currentUser.PasswordHash))
            {
                return BadRequest("Invalid user credentials");
            }

            var token = jwtAuthManager.FetchToken(currentUser);

            return Ok(token);
        }

        [Authorize]
        [HttpPost(nameof(RefreshToken))]
        public IActionResult RefreshToken()
        {
            ClaimsPrincipal currentUser = Request.HttpContext.User;
            Claim sidClaim = currentUser.Claims.First(x => x.Type.Equals(ClaimTypes.Sid));
            string currentUserUsername = sidClaim.Value;

            User user = userService.GetUserByUsername(currentUserUsername);

            string updatedJwtToken = jwtAuthManager.FetchToken(user);
            return Ok(updatedJwtToken);
        }
    }
}
