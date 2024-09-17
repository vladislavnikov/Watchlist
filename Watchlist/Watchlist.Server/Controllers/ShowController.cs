using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Watchlist.Core.Common;
using Watchlist.Core.Contracts;
using Watchlist.Core.DTOs.Show;
using Watchlist.Infrastructure.Data.Models;

namespace Watchlist.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShowController : Controller
    {
        private readonly IMapper mapper;
        private readonly IShowRepository showRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ShowController(IMapper _mapper, IShowRepository _showRepository, UserManager<ApplicationUser> _userManager)
        {
            this.mapper = _mapper;
            this.showRepository = _showRepository;
            this.userManager = _userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShowDto>))]
        public IActionResult GetShows()
        {
            var shows = showRepository.GetAllShows();
            var showDtos = mapper.Map<List<ShowDto>>(shows);

            if (showDtos == null)
            {
                return BadRequest(Messages.NoShows);
            }

            return Ok(showDtos);
        }

        [HttpGet("{showId}")]
        [ProducesResponseType(200, Type = typeof(ShowDto))]
        [ProducesResponseType(400)]
        public IActionResult GetShow(int showId)
        {
            var show = showRepository.GetShow(showId);

            var showDto = mapper.Map<ShowDto>(show);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(showDto);
        }

        [HttpGet("usershows")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShowDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserShows()
        {
            var user = await userManager.GetUserAsync(User);
            var Shows = showRepository.GetUserShows(user.Id);
            var ShowDtos = mapper.Map<List<ShowDto>>(Shows);

            return Ok(ShowDtos);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddShowToUser(int showId)
        {
            var user = await userManager.GetUserAsync(User);
            await showRepository.AddShowToUserCollectionAsync(showId, user.Id);

            return Ok();
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveShowFromUser(int showId)
        {
            var user = await userManager.GetUserAsync(User);
            await showRepository.RemoveShowToUserCollectionAsync(showId, user.Id);

            return Ok();
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateShow([FromBody] CreateShowDto showModel)
        {
            if (showModel == null)
            {
                return BadRequest(ModelState);
            }

            if (showRepository.ShowExistsByTitle(showModel.Title))
            {
                ModelState.AddModelError("", Messages.ShowExists);
                return StatusCode(422, ModelState);
            }

            var show = mapper.Map<Show>(showModel);
            await showRepository.CreateShowAsync(show);

            return Ok();
        }

        [HttpPut("{showId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateShow(int showId, [FromBody] ShowDto updatedShow)
        {
            if (updatedShow == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var Show = mapper.Map<Show>(updatedShow);
            await showRepository.UpdateShowAsync(showId, Show);

            return NoContent();
        }

        [HttpDelete("{showId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteShow(int showId)
        {
            await showRepository.DeleteShowAsync(showId);
            return NoContent();
        }
    }
}
