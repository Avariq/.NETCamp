using AnimeLib.API.Models;
using AnimeLib.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeLib.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Anime, AnimeDTO>();
            CreateMap<AnimeDTO, Anime>();
        }

    }
}
