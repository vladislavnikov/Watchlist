using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Watchlist.Core.Common;
using Watchlist.Core.Contracts;
using Watchlist.Core.DTOs.Movie;
using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public MovieController(IMapper mapper, IMovieRepository movieRepository, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Movie>))] 
        public IActionResult GetMovies()
        {
            var movies = _movieRepository.GetAllMovies();
            var movieDtos = _mapper.Map<List<MovieDto>>(movies);
            //

            if (movieDtos == null)
            {
                return BadRequest(Messages.NoMovies);
            }

            return Ok(movieDtos);
        }

        [HttpGet("{movieId}")]
        [ProducesResponseType(200, Type = typeof(MovieDto))]
        [ProducesResponseType(400)]
        public IActionResult GetMovie(int movieId)
        {
            var movie = _movieRepository.GetMovie(movieId);
            var movieDto = _mapper.Map<MovieDto>(movie);
            //

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(movieDto);
        }

        [HttpGet("usermovies")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MovieDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserMovies()
        {
            var user = await _userManager.GetUserAsync(User);
            var movies = _movieRepository.GetUserMovies(user.Id);
            var movieDtos = _mapper.Map<List<MovieDto>>(movies);

            return Ok(movieDtos);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMovieToUser([FromBody] int movieId)
        {
            var user = await _userManager.GetUserAsync(User);
            await _movieRepository.AddMovieToUserCollectionAsync(movieId, user.Id);

            return Ok();
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveMovieFromUser(int movieId)
        {
            var user = await _userManager.GetUserAsync(User);
            await _movieRepository.RemoveMovieToUserCollectionAsync(movieId, user.Id);

            return Ok();
        }

        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateMovie([FromBody] CreateMovieDto movieModel)
        {
            if (movieModel == null)
            {
                return BadRequest(ModelState);
            }

            if (_movieRepository.MovieExistsByTitle(movieModel.Title))
            {
                ModelState.AddModelError("", Messages.MovieExists);
                return StatusCode(422, ModelState);
            }

            var movie = _mapper.Map<Movie>(movieModel);
            await _movieRepository.CreateMovieAsync(movie);

            return Ok();
        }

        [HttpPut("{movieId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateMovie([FromBody] UpdateMovieDto updatedMovie)
        {
            if (updatedMovie == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var movie = _mapper.Map<Movie>(updatedMovie);
            await _movieRepository.UpdateMovieAsync(updatedMovie.Id, movie);

            return NoContent();
        }

        [HttpDelete("{movieId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteMovie(int movieId)
        {
            await _movieRepository.DeleteMovieAsync(movieId);
            return NoContent();
        }
    }
}
