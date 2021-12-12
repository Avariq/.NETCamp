using AnimeLib.Domain.Models;
using AnimeLib.Services;
using AnimeLib.Services.Exceptions.Root_exceptions;
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
        public IActionResult GetEpisodeById([FromQuery] int epId)
        {
            try
            {
                logger.LogInformation($"Getting episode by id: {epId}");
                Episode ep = animeService.GetEpisodeById(epId);

                return Ok(ep);
            }
            catch (AnimeServiceException e)
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

        [HttpGet(nameof(GetEpisodeId))]
        [AllowAnonymous]
        public IActionResult GetEpisodeId([FromQuery] int arcId, string epName)
        {
            try
            {
                logger.LogInformation($"Getting episode id by arcId: {arcId} and episode name: {epName}");
                int id = animeService.GetEpisodeId(arcId, epName);

                return Ok(id);
            }
            catch (AnimeServiceException e)
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

        [Authorize(Roles = "Admin")]
        [HttpPost(nameof(CreateEpisode))]
        public ActionResult<Episode> CreateEpisode(Episode episode)
        {
            try
            {
                logger.LogInformation("Creating episode");

                if (episode == null)
                {
                    return BadRequest();
                }

                var createdEpisode = animeService.CreateEpisode(episode);
                return CreatedAtAction(nameof(GetEpisodeById), new { id = createdEpisode.Id }, createdEpisode);
            }
            catch (AnimeServiceException e)
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
    }
}
