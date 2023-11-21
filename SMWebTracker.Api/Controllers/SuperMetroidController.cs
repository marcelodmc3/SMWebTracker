using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMWebTracker.Data;
using SMWebTracker.Domain.Dtos;
using SMWebTracker.Domain.Entities;
using SMWebTracker.Domain.Interfaces;
using System.Security.Claims;

namespace SMWebTracker.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SuperMetroidController : ControllerBase
    {
        private readonly ISuperMetroidGameService _superMetroidGameService;

        public SuperMetroidController(ISuperMetroidGameService superMetroidGameService)
        {
            _superMetroidGameService = superMetroidGameService;
        }

        [HttpPost("CreateNewGame")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNewGame([FromBody] NewSuperMetroidGameParameters parameters)
        {
            if (parameters != null && parameters.PlayerNames != null && parameters.PlayerNames.All(p => !string.IsNullOrWhiteSpace(p) && p.Length > 2))
            {
                var result = await _superMetroidGameService.CreateNewGameAsync(parameters, User.FindFirst(ClaimTypes.Email)?.Value);
                if (result != null)
                {
                    return Ok(result);
                }
            }
            
            return BadRequest("Por favor preencha a lista de usuários");
        }

        [HttpGet("game/{gameId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGame([FromRoute] Guid gameId)
        {           
            var result = await _superMetroidGameService.GetGameAsNoTrackingAsync(gameId);
            if (result != null)
                return Ok(result);
            
            return NotFound();
        }

        [HttpGet("tracker/{trackerId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TrackItem([FromRoute] Guid trackerId)
        {
            var result = await _superMetroidGameService.GeTrackerAsNoTrackingAsync(trackerId);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpGet("tracker/{gameId}/{trackerIndex}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTrackerByIndex([FromRoute] Guid gameId, [FromRoute] int trackerIndex)
        {
            var result = await _superMetroidGameService.GeTrackerAsNoTrackingAsync(gameId, trackerIndex);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPatch("tracker/{trackerId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TrackItem([FromRoute] Guid trackerId, [FromBody] SuperMetroidTrackerModel tracker)
        {
            var result = await _superMetroidGameService.Track(trackerId, tracker);
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPatch("tracker/{gameId}/{trackerIndex}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TrackItemByIndex([FromRoute] Guid gameId, [FromRoute] int trackerIndex, [FromBody] SuperMetroidTrackerModel tracker)
        {
            var result = await _superMetroidGameService.Track(gameId, trackerIndex, tracker);
            if (result != null)
                return Ok(result);

            return NotFound();
        }
    }
}
