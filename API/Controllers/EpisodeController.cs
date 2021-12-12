using AnimeLib.Domain.Models;
using AnimeLib.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeLib.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly ILogger<EpisodeController> logger;
        private readonly IAnimeService animeService;

        public EpisodeController(ILogger<EpisodeController> _logger, IAnimeService _animeService)
        {
            logger = _logger;
            animeService = _animeService;
        }

        [HttpGet(nameof(GetEpisodeById))]
        [AllowAnonymous]
        public IActionResult GetEpisodeById([FromQuery] int id)
        {
            Episode ep = animeService.GetEpisodeById(id);

            return Ok(ep);
        }

        [HttpGet(nameof(GetEpisodeId))]
        [AllowAnonymous]
        public IActionResult GetEpisodeId([FromQuery] int arcId, string epName)
        {
            int id = animeService.GetEpisodeId(arcId, epName);

            /*may return -1*/
            /*Exception handling is crucial*/

            return Ok(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost(nameof(CreateEpisode))]
        public ActionResult<Episode> CreateEpisode(Episode episode)
        {
            try
            {
                if (episode == null)
                {
                    return BadRequest();
                }

                var createdEpisode = animeService.CreateEpisode(episode);
                return CreatedAtAction(nameof(GetEpisodeById), new { id = createdEpisode.Id }, createdEpisode);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured while creating new Episode");
            }
        }
    }
}
