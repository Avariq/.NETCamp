using AnimeLib.Domain.DataAccess;
using AnimeLib.Domain.Models;
using AnimeLib.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        [HttpGet]
        public IActionResult Get()
        {
            List<Anime> animes = animeService.GetAllAnimes();
            return Ok(animes);
        }

        [HttpGet("GetByTitle/{fragment}")]
        public IActionResult GetByTitle(string fragment)
        {
            List<Anime> animes = animeService.GetAnimesByTitle(fragment);
            if (animes.Count < 1)
            {
                return NoContent();
            }
            return Ok(animes);
        }

        // GET api/<MainController>/5
        [HttpGet("GetById/{id}")]
        public string Get(int id)
        {
            return "value";
        }

    }
}
