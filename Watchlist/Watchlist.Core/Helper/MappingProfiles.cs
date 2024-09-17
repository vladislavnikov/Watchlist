using AutoMapper;
using Watchlist.Core.DTOs.Director;
using Watchlist.Core.DTOs.Movie;
using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Core.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<Movie, CreateMovieDto>().ReverseMap();

            CreateMap<Director, DirectorDto>().ReverseMap();
            CreateMap<Director, CreateDirectorDto>().ReverseMap();

        }
    }
}
