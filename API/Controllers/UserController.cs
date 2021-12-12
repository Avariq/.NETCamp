using AnimeLib.API.Config.Auth;
using AnimeLib.API.Models.Input;
using AnimeLib.Domain.Models;
using AnimeLib.Services;
using AnimeLib.Services.Exceptions.Root_exceptions;
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
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IJwtAuthenticationManager jwtAuthManager;

        public UserController(ILogger<UserController> _logger, IUserService _userService, IMapper _mapper, IJwtAuthenticationManager _jwtAuthManager)
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
            try
            {
                logger.LogInformation("User is Logging in");

                User currentUser = userService.GetUserByUsername(userCredentials.Username);

                if (!userCredentials.PasswordHash.Equals(currentUser.PasswordHash))
                {
                    return BadRequest("Invalid user credentials");
                }

                var token = jwtAuthManager.FetchToken(currentUser);

                return Ok(token);
            }
            catch (UserServiceException e)
            {
                logger.LogWarning(e.Message);
                return StatusCode(e.StatusCode, e.Message);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
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
