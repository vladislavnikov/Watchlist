using AutoMapper;
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

        }
    }
}
