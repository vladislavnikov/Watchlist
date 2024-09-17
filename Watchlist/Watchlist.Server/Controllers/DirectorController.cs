using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Watchlist.Core.Common;
using Watchlist.Core.Contracts;
using Watchlist.Core.DTOs.Director;
using Watchlist.Core.DTOs.Movie;
using Watchlist.Core.Repository;
using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDirectorRepository _directorRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public DirectorController(IMapper mapper, IDirectorRepository directorRepository, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _directorRepository = directorRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Director>))]
        public IActionResult GetDirectors()
        {
            var directors = _directorRepository.GetAllDirectors();
            var directorDtos = _mapper.Map<List<DirectorDto>>(directors);

            if (directorDtos == null)
            {
                return BadRequest(Messages.NoDirectors);
            }

            return Ok(directorDtos);
        }

        [HttpGet("{directorId}")]
        [ProducesResponseType(200, Type = typeof(DirectorDto))]
        [ProducesResponseType(400)]
        public IActionResult GetDirector(int directorId)
        {
            var director = _directorRepository.GetDirector(directorId);
            var directorDto = _mapper.Map<DirectorDto>(director);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(directorDto);
        }

        [HttpPost("create")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateDirector([FromBody] CreateDirectorDto directorModel)
        {
            if (directorModel == null)
            {
                return BadRequest(ModelState);
            }

            if (_directorRepository.DirectorExistsByName(directorModel.Name))
            {
                ModelState.AddModelError("", Messages.DirectorExists);
                return StatusCode(422, ModelState);
            }

            var directorMap = _mapper.Map<Director>(directorModel);

            await _directorRepository.CreateDirectorAsync(directorMap);

            return Ok();
        }

        [HttpPut("{directorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateDirector(int directorId, [FromBody] DirectorDto updatedDirector)
        {
            if (updatedDirector == null)
            {
                return BadRequest(ModelState);
            }

            if (directorId != updatedDirector.Id)
            {
                return BadRequest(Messages.NoSameIds);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var directorMap = _mapper.Map<Director>(updatedDirector);

            await _directorRepository.UpdateDirectorAsync(directorId, directorMap);

            return Ok();
        }

        [HttpDelete("{directorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteDirector(int directorId)
        {
            await _directorRepository.DeleteDirectorAsync(directorId);

            return Ok();
        }
    }
}
