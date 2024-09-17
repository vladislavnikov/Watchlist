using AutoMapper;
using Watchlist.Core.DTOs.Director;
using Watchlist.Core.DTOs.Movie;
using Watchlist.Core.DTOs.Review;
using Watchlist.Core.DTOs.Show;
using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Core.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Movie, MovieDto>()
            .ForMember(dest => dest.DirectorName, opt => opt.MapFrom(src => src.Director.Name)) 
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.ToString())) 
            .ReverseMap();

            CreateMap<Movie, UpdateMovieDto>().ReverseMap();
            CreateMap<Movie, CreateMovieDto>().ReverseMap();

            CreateMap<Director, DirectorDto>().ReverseMap();
            CreateMap<Director, CreateDirectorDto>().ReverseMap();

            CreateMap<Show, ShowDto>()
                .ForMember(dest => dest.DirectorName, opt => opt.MapFrom(src => src.Director.Name))
                .ReverseMap();

            CreateMap<Show, UpdateShowDto>().ReverseMap();
            CreateMap<Show, CreateShowDto>().ReverseMap();

            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, CreateReviewDto>().ReverseMap();

        }
    }
}
