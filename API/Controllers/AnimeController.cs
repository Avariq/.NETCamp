using AnimeLib.API.Models;
using AnimeLib.API.Models.Output;
using AnimeLib.Domain.DataAccess;
using AnimeLib.Domain.Models;
using AnimeLib.Services;
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
        private readonly AnimeService animeService;
        private readonly IMapper mapper;

        public AnimeController(ILogger<AnimeController> _logger, AnimeService _animeService, IMapper _mapper)
        {
            logger = _logger;
            animeService = _animeService;
            mapper = _mapper;
        }

        [HttpGet(nameof(GetRecent))]
        [AllowAnonymous]
        public IActionResult GetRecent([FromQuery] PageArgs pageArgs)
        {
            AnimeArgsOut animesOutput = new();
            int animeAmount = animeService.GetAnimeAmount();
            double temp = Math.Ceiling((double)animeAmount / (double)pageArgs.pageSize);
            int totalPages = (int)temp;
            int toSkip = (totalPages - pageArgs.pageNumber) * pageArgs.pageSize;
            int toTake = pageArgs.pageSize;

            if (pageArgs.pageNumber == 1)
            {
                toSkip = animeAmount - pageArgs.pageSize;
            }

            if (toSkip == 0)
            {
                toTake = animeAmount - (pageArgs.pageNumber - 1) * pageArgs.pageSize;
            }


            animesOutput.animes = animeService.GetRecent(toSkip, toTake);
            animesOutput.totalAmount = animeAmount;

            return Ok(animesOutput);
        }

        [HttpPost(nameof(GetByFilter))]
        [AllowAnonymous]
        public IActionResult GetByFilter([FromBody] FilterBody[] filters)
        {
            IQueryable<Anime> animeData = animeService.GetAllAnimesQueryable();

            foreach (FilterBody filter in filters)
            {
                animeData = animeData.Apply(filter);
            }

            return Ok(animeData.ToArray());
        }

        [HttpGet(nameof(GetAnimeIdByTitle))]
        [AllowAnonymous]
        public IActionResult GetAnimeIdByTitle([FromQuery] string title)
        {
            int returnedId = animeService.GetAnimeId(title);

            return Ok(returnedId);
        }

        [HttpGet(nameof(GetAnimeTitles))]
        [AllowAnonymous]
        public IActionResult GetAnimeTitles()
        {
            string[] titles = animeService.GetAllAnimeTitles();

            return Ok(titles);
        }

        [HttpGet(nameof(GetAnimeById))]
        [AllowAnonymous]
        public IActionResult GetAnimeById([FromQuery] int animeId)
        {
            ClaimsPrincipal currentUser = Request.HttpContext.User;
            if (!currentUser.Identity.IsAuthenticated)
            {
                Console.WriteLine("HAha");
            }
            Anime anime = animeService.GetAnimeById(animeId);

            return Ok(anime);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost(nameof(CreateAnime))]
        public ActionResult<Anime> CreateAnime([FromBody] AnimeArgs inputAnime)
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

        [HttpGet(nameof(GetStatusId))]
        [AllowAnonymous]
        public IActionResult GetStatusId([FromQuery] string statusName)
        {
            int id = animeService.GetStatusId(statusName);

            return Ok(id);
        }

        [HttpGet(nameof(GetAgeRestrictionId))]
        [AllowAnonymous]
        public IActionResult GetAgeRestrictionId(string ageRestrictionCode)
        {
            int id = animeService.GetAgeRestrictionId(ageRestrictionCode);

            return Ok(id);
        }

        [HttpGet(nameof(GetAllGenres))]
        [AllowAnonymous]
        public IActionResult GetAllGenres()
        {
            var genres = animeService.GetAllGenres();

            return Ok(genres);
        }

    }
}
