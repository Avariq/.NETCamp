using AnimeLib.Domain.DataAccess;
using AnimeLib.Domain.Models;
using AnimeLib.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*    async Task<ActionResult> + Exception handling    */

namespace AnimeLib.API.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class MainController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly AnimeService animeService;

        public MainController(ILogger<MainController> _logger, AnimeService _animeService)
        {
            logger = _logger;
            animeService = _animeService;
        }

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            Anime[] animes = animeService.GetAllAnimes();
            return Ok(animes);
        }

        [HttpGet(nameof(GetByTitle) + "/{titlefragment}")]
        public IActionResult GetByTitle(string titlefragment)
        {
            Anime[] animes = animeService.GetAnimesByTitle(titlefragment);
            if (animes.Length < 1)
            {
                return NoContent();
            }
            return Ok(animes);
        }

        [HttpGet(nameof(GetAnimeIdByTitle) + "/{title}")]
        public IActionResult GetAnimeIdByTitle(string title)
        {
            int returnedId = animeService.GetAnimeId(title);

            return Ok(returnedId);
        }

        [HttpGet(nameof(GetAnimeTitles))]
        public IActionResult GetAnimeTitles()
        {
            string[] titles = animeService.GetAllAnimeTitles();

            return Ok(titles);
        }

        [HttpGet(nameof(GetArcs) + "/{animeId}")]
        public IActionResult GetArcs(int animeId)
        {
            string[] titles = animeService.GetArcTitlesByAnimeId(animeId);

            return Ok(titles);
        }

        [HttpGet(nameof(GetArcById) + "/{id}")]
        public IActionResult GetArcById(int id)
        {
            Arc arc = animeService.GetArcById(id);
            
            return Ok(arc);
        }

        [HttpGet(nameof(GetArcId) + "/{arcName}")]
        public IActionResult GetArcId(string arcName)
        {
            int id = animeService.GetArcId(arcName);

            return Ok(id);
        }

        [HttpPost(nameof(CreateArc))]
        public async Task<ActionResult<Arc>> CreateArc(Arc arc)
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

        [HttpGet(nameof(GetEpisodeById) + "/{id}")]
        public IActionResult GetEpisodeById(int id)
        {
            Episode ep = animeService.GetEpisodeById(id);

            return Ok(ep);
        }

        [HttpGet(nameof(GetEpisodeId) + "/{arcId}/{epName}")]
        public IActionResult GetEpisodeId(int arcId, string epName)
        {
            int id = animeService.GetEpisodeId(arcId, epName);

            /*may return -1*/
            /*Exception handling is crucial*/

            return Ok(id);
        }

        [HttpPost(nameof(CreateEpisode))]
        public async Task<ActionResult<Episode>> CreateEpisode(Episode episode)
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
