using AnimeLib.API.Config.Auth;
using AnimeLib.API.Models.Input;
using AnimeLib.Domain.Models;
using AnimeLib.Services;
using AnimeLib.Services.Exceptions.Root_exceptions;
using AutoMapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
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

        [HttpPost(nameof(SignUp))]
        [AllowAnonymous]
        public IActionResult SignUp([FromBody] UserDto newUserData)
        {
            try
            {
                logger.LogInformation("Creating new user.");
                User createdUser = userService.CreateUser(newUserData.Username, newUserData.Email, newUserData.PasswordHash);

                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
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

        [HttpGet(nameof(GetUserById))]
        [AllowAnonymous]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                logger.LogInformation($"Getting user by id: {userId}");
                User user = userService.GetUserById(userId);

                return Ok(user);

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

        [AllowAnonymous]
        [HttpPost(nameof(SendTestEmailHotmail))]
        public async Task<IActionResult> SendTestEmailHotmail()
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("sportshub.service@hotmail.com"));
                email.To.Add(MailboxAddress.Parse("nickyr.beast@gmail.com"));
                email.Subject = "Test Email Subject";
                email.Body = new TextPart(TextFormat.Plain) { Text = "Example Plain Text Message Body" };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.live.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("sportshub.service@hotmail.com", "steamisjustavaporizedwater123");

                await smtp.SendAsync(email);

                smtp.Disconnect(true);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [AllowAnonymous]
        [HttpPost(nameof(SendTestEmailYahoo))]
        public async Task<IActionResult> SendTestEmailYahoo()
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("sportshub.service@yahoo.com"));
                email.To.Add(MailboxAddress.Parse("nickyr.beast@gmail.com"));
                email.Subject = "Test Email Subject";
                email.Body = new TextPart(TextFormat.Plain) { Text = "Example Plain Text Message Body" };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.mail.yahoo.com", 465, SecureSocketOptions.SslOnConnect);
                smtp.Authenticate("sportshub.service@yahoo.com", "V!jfeGbJBARVJ5$");

                await smtp.SendAsync(email);

                smtp.Disconnect(true);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [AllowAnonymous]
        [HttpPost(nameof(SendTestEmailGoogle))]
        public async Task<IActionResult> SendTestEmailGoogle()
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("sporthub.mailservice@gmail.com"));
                email.To.Add(MailboxAddress.Parse("nickyr.beast@gmail.com"));
                email.Subject = "Test Email Subject";
                email.Body = new TextPart(TextFormat.Plain) { Text = "Example Plain Text Message Body" };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                smtp.Authenticate("sporthub.mailservice@gmail.com", "jzwitngbskeqrkjd");

                await smtp.SendAsync(email);

                smtp.Disconnect(true);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
