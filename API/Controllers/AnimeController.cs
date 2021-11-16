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

        [HttpGet(nameof(GetRecent))]
        public IActionResult GetRecent([FromQuery] PageArgs pageArgs)
        {
            Anime[] animes = animeService.GetRecent(pageArgs.pageNumber, pageArgs.pageSize);

            return Ok(animes);
        }

        [HttpGet(nameof(GetByFilter))]
        public IActionResult GetByFilter([FromQuery] FilterArgs filterArgs)
        {
            if (filterArgs.statusIds[0] == -1) filterArgs.statusIds = animeService.GetAllStatusIds();
            if (filterArgs.arIds[0] == -1) filterArgs.arIds = animeService.GetAllARIds();
            if (filterArgs.from_year == 0) filterArgs.from_year = 1900;
            if (filterArgs.to_year == 0) filterArgs.to_year = DateTime.Now.Year + 10;
            if (filterArgs.genreIds[0] == -1) filterArgs.genreIds = null;

            /*Спитати чи норм передавати FilterArgs в контекст БД і вже там розбирати чи крaще купкою розділених аргументів зразу*/

            List<Anime> animesRetrieved = animeService.GetAnimesByFilter(
                filterArgs.statusIds, filterArgs.arIds, filterArgs.from_year, filterArgs.to_year, filterArgs.genreIds,
                filterArgs.pageNumber, filterArgs.pageSize
                );

            List<Anime> animes = filterArgs.orderBy switch
            {
                0 => animesRetrieved.OrderBy(a => a.Title).ToList(),
                1 => animesRetrieved.OrderBy(a => a.Year).ToList(),
                2 => animesRetrieved.OrderBy(a => a.Rating).ToList(),
                3 => animesRetrieved.OrderBy(a => a.Votes).ToList(),
                4 => animesRetrieved.OrderBy(a => a.Views).ToList(),
                _ => animesRetrieved,
            };

            if (filterArgs.isDescending) animes.Reverse();

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
