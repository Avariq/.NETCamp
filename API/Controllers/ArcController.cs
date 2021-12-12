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
            string[] titles = animeService.GetArcTitlesByAnimeId(animeId);

            return Ok(titles);
        }

        [HttpGet(nameof(GetArcById) + "/{id}")]
        [AllowAnonymous]
        public IActionResult GetArcById(int id)
        {
            Arc arc = animeService.GetArcById(id);

            return Ok(arc);
        }

        [HttpGet(nameof(GetArcId))]
        [AllowAnonymous]
        public IActionResult GetArcId([FromQuery] string arcName)
        {
            int id = animeService.GetArcId(arcName);

            return Ok(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost(nameof(CreateArc))]
        public ActionResult<Arc> CreateArc(Arc arc)
        {
            try
            {
                if (arc == null)
                {
                    return BadRequest();
                }

                var createdArc = animeService.CreateArc(arc);
                return CreatedAtAction(nameof(GetArcById), new { id = createdArc.Id }, createdArc);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured while creating new Arc");
            }
        }
    }
}
