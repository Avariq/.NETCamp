using AnimeLib.API.Models;
using AnimeLib.Domain.DataAccess;
using AnimeLib.Domain.Models;
using AnimeLib.Services;
using AutoMapper;
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
    [Route("api/[controller]")]
    public class AnimeController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly AnimeService animeService;
        private readonly IMapper mapper;

        public AnimeController(ILogger<AnimeController> _logger, AnimeService _animeService, IMapper _mapper)
        {
            logger = _logger;
            animeService = _animeService;
            mapper = _mapper;
        }

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            Anime[] animes = animeService.GetAllAnimes();
            return Ok(animes);
        }

        [HttpGet(nameof(GetByFilter))]
        public IActionResult GetByFilter([FromQuery] FilterArgs args)
        {
            if (args.statusIds[0] == -1) args.statusIds = animeService.GetAllStatusIds();
            if (args.arIds[0] == -1) args.arIds = animeService.GetAllARIds();
            if (args.from_year == 0) args.from_year = 1900;
            if (args.to_year == 0) args.to_year = DateTime.Now.Year + 10;
            if (args.genreIds[0] == -1) args.genreIds = null;

            List<Anime> animes = animeService.GetAnimesByFilter(args.statusIds, args.arIds, args.from_year, args.to_year, args.genreIds);

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

        [HttpGet(nameof(GetAnimeById) + "/{animeId}")]
        public IActionResult GetAnimeById(int animeId)
        {
            Anime anime = animeService.GetAnimeById(animeId);

            return Ok(anime);
        }

        [HttpPost(nameof(CreateAnime))]
        public async Task<ActionResult<Anime>> CreateAnime([FromBody] AnimeArgs inputAnime)
        {
            try
            {
                if (inputAnime == null)
                {
                    return BadRequest();
                }

                var anime = mapper.Map<Anime>(inputAnime.Anime);

                var createdAnime = animeService.CreateAnime(anime, inputAnime.Genres);
                Console.WriteLine(createdAnime.Id);
                return CreatedAtAction(nameof(GetAnimeById), new { id = createdAnime.Id }, createdAnime);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error occured while creating new Anime");
            }
        }

        [HttpGet(nameof(GetStatusId) + "/{statusName}")]
        public IActionResult GetStatusId(string statusName)
        {
            int id = animeService.GetStatusId(statusName);

            return Ok(id);
        }

        [HttpGet(nameof(GetAgeRestrictionId) + "/{ageRestrictionCode}")]
        public IActionResult GetAgeRestrictionId(string ageRestrictionCode)
        {
            int id = animeService.GetAgeRestrictionId(ageRestrictionCode);

            return Ok(id);
        }

        [HttpGet(nameof(GetAllGenres))]
        public IActionResult GetAllGenres()
        {
            var genres = animeService.GetAllGenres();

            return Ok(genres);
        }

    }
}
