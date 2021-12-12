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
    public class ArcController : ControllerBase
    {
        private readonly ILogger<ArcController> logger;
        private readonly IAnimeService animeService;

        public ArcController(ILogger<ArcController> _logger, IAnimeService _animeService)
        {
            logger = _logger;
            animeService = _animeService;
        }

        [HttpGet(nameof(GetArcs))]
        [AllowAnonymous]
        public IActionResult GetArcs([FromQuery] int animeId)
        {
            try
            {
                logger.LogInformation($"Getting arcs by anime id: {animeId}");
                string[] titles = animeService.GetArcTitlesByAnimeId(animeId);

                return Ok(titles);
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

        [HttpGet(nameof(GetArcById))]
        [AllowAnonymous]
        public IActionResult GetArcById([FromQuery] int arcId)
        {
            try
            {
                logger.LogInformation($"Getting Arc by id: {arcId}");
                Arc arc = animeService.GetArcById(arcId);

                return Ok(arc);
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

        [HttpGet(nameof(GetArcId))]
        [AllowAnonymous]
        public IActionResult GetArcId([FromQuery] string arcName, int animeId)
        {
            try
            {
                logger.LogInformation($"Getting Arc Id by arcName: {arcName}");
                int id = animeService.GetArcId(arcName, animeId);

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
        [HttpPost(nameof(CreateArc))]
        public ActionResult<Arc> CreateArc(Arc arc)
        {
            try
            {
                logger.LogInformation("Creating new arc");
                if (arc == null)
                {
                    return BadRequest();
                }

                var createdArc = animeService.CreateArc(arc);
                logger.LogInformation("Arc has been successfully created");
                return CreatedAtAction(nameof(GetArcById), new { id = createdArc.Id }, createdArc);
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
