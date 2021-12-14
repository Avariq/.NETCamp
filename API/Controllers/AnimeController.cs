using AnimeLib.API.Models;
using AnimeLib.API.Models.Input;
using AnimeLib.API.Models.Output;
using AnimeLib.Domain.DataAccess;
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
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;


namespace AnimeLib.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IAnimeService animeService;
        private readonly IMapper mapper;

        public AnimeController(ILogger<AnimeController> _logger, IAnimeService _animeService, IMapper _mapper)
        {
            logger = _logger;
            animeService = _animeService;
            mapper = _mapper;
        }

        [HttpGet(nameof(GetRecent))]
        [AllowAnonymous]
        public IActionResult GetRecent([FromQuery] PageArgs pageArgs)
        {
            try
            {
                logger.LogInformation("Getting recent animes");

                AnimeArgsOut animesOutput = new();
                int animeAmount = animeService.GetAnimeAmount();


                animesOutput.animes = animeService.GetRecent(animeAmount, pageArgs.pageNumber, pageArgs.pageSize);
                animesOutput.totalAmount = animeAmount;

                logger.LogInformation("Successfully got recent animes");
                return Ok(animesOutput);
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

        [HttpPost(nameof(GetByFilter))]
        [AllowAnonymous]
        public IActionResult GetByFilter([FromBody] FilterBody[] filters)
        {
            try
            {
                logger.LogInformation("Getting animes by filters");

                IQueryable<Anime> animeData = animeService.GetAllAnimesQueryable();

                foreach (FilterBody filter in filters)
                {
                    animeData = animeData.Apply(filter);
                }

                return Ok(animeData.ToArray());
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

        [HttpGet(nameof(GetAnimeIdByTitle))]
        [AllowAnonymous]
        public IActionResult GetAnimeIdByTitle([FromQuery] string title)
        {
            try
            {
                logger.LogInformation($"Getting anime id by title: {title}");
                int returnedId = animeService.GetAnimeId(title);

                return Ok(returnedId);
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

        [HttpGet(nameof(GetAnimeTitles))]
        [AllowAnonymous]
        public IActionResult GetAnimeTitles()
        {
            logger.LogInformation("Anime titles are retrieved");
            string[] titles = animeService.GetAllAnimeTitles();

            return Ok(titles);
        }

        [HttpGet(nameof(GetAnimeById))]
        [AllowAnonymous]
        public IActionResult GetAnimeById([FromQuery] int animeId)
        {
            try
            {
                logger.LogInformation($"Getting anime by id: {animeId}");
                Anime anime = animeService.GetAnimeById(animeId);

                return Ok(anime);
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
        [HttpPost(nameof(CreateAnime))]
        public ActionResult<Anime> CreateAnime([FromBody] AnimeArgs inputAnime)
        {
            try
            {
                logger.LogInformation("Creating anime");
                if (inputAnime == null)
                {
                    return BadRequest();
                }

                var anime = mapper.Map<Anime>(inputAnime.Anime);

                var createdAnime = animeService.CreateAnime(anime, inputAnime.Genres);
                logger.LogInformation("Anime has been successfully created");
                return CreatedAtAction(nameof(GetAnimeById), new { id = createdAnime.Id }, createdAnime);
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

        [HttpGet(nameof(GetStatusId))]
        [AllowAnonymous]
        public IActionResult GetStatusId([FromQuery] string statusName)
        {
            try
            {
                logger.LogInformation($"Getting status id by name: {statusName}");
                int id = animeService.GetStatusId(statusName);

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

        [HttpGet(nameof(GetAgeRestrictionId))]
        [AllowAnonymous]
        public IActionResult GetAgeRestrictionId(string ageRestrictionCode)
        {
            try
            {
                logger.LogInformation($"Getting age restriction id by arCode: {ageRestrictionCode}");
                int id = animeService.GetAgeRestrictionId(ageRestrictionCode);

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

        [HttpGet(nameof(GetAllGenres))]
        [AllowAnonymous]
        public IActionResult GetAllGenres()
        {
            var genres = animeService.GetAllGenres();

            return Ok(genres);
        }

        [HttpGet(nameof(GetRandomAnime))]
        [AllowAnonymous]
        public IActionResult GetRandomAnime()
        {
            try
            {
                var anime = animeService.GetRandomAnime();
                return Ok(anime);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }

        /*[Authorize(Roles = "Admin")]*/
        [HttpDelete(nameof(DeleteAnimeByTitle))]
        public IActionResult DeleteAnimeByTitle(string animeTitle)
        {
            try
            {
                logger.LogInformation("Deleting anime");
                animeService.DeleteAnimeByTitle(animeTitle);
                return Ok();
            }
            catch (Exception)
            {
                logger.LogWarning("Unsuccessful");
                return BadRequest("Invalid paramaters");
            }
        }
    }
}
