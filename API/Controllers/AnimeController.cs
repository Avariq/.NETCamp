using AnimeLib.API.Models;
using AnimeLib.API.Models.Output;
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
using System.Reflection;
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
            AnimeArgsOut animesOutput = new();

            animesOutput.animes = animeService.GetRecent(pageArgs.pageNumber, pageArgs.pageSize);
            animesOutput.totalAmount = animeService.GetAnimeAmount();

            return Ok(animesOutput);
        }

        [HttpPost(nameof(GetByFilter))]
        public IActionResult GetByFilter([FromBody] FilterBody[] filters)
        {
            IQueryable<Anime> animeData = animeService.GetAllAnimesQueryable();

            foreach (var filter in filters)
            {
                object[] arguments = { animeData, filter };
                string filterMethod = "Apply" + filter.Name;

                Type type = typeof(FilterManager);
                MethodInfo methodInfo = type.GetMethod(filterMethod);

                animeData = (IQueryable<Anime>)methodInfo.Invoke(filterMethod, arguments);
            }
            

            return Ok(animeData.ToArray());
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

        [HttpGet(nameof(GetAnimeById))]
        public IActionResult GetAnimeById([FromQuery] int animeId)
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
