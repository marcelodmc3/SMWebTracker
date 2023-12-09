using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMWebTracker.Domain.Dtos;
using SMWebTracker.Domain.Interfaces;
using System.Security.Claims;

namespace SMWebTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            if (parameters != null && parameters.PlayerNames != null && parameters.PlayerNames.All(p => !string.IsNullOrWhiteSpace(p.Trim()) && p.Trim().Length > 2))
            {
                if (!string.IsNullOrWhiteSpace(parameters.Description) && parameters.Description.Trim().Length > 2)
                {
                    var result = await _superMetroidGameService.CreateNewGameAsync(parameters, User.FindFirst(ClaimTypes.Email)?.Value);
                    if (result != null)
                    {
                        return Ok(result);
                    }                    
                }
            }
            
            return BadRequest("Nomes e descrição precisam ter pelo menos 3 caracteres.");
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

        [HttpGet("game/active")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetActiveGames()
        {
            var result = await _superMetroidGameService.GetActiveGamesAsNoTrackingAsync();
            if (result != null)
                return Ok(result);

            return NotFound();
        }

        [HttpPatch("game/{gameId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateGameInfo([FromRoute] Guid gameId, [FromBody] NewSuperMetroidGameParameters parameters)
        {
            if (parameters != null && parameters.PlayerNames != null && parameters.PlayerNames.Any() && parameters.PlayerNames.All(p => !string.IsNullOrWhiteSpace(p.Trim()) && p.Trim().Length > 2))
            {
                if (!string.IsNullOrWhiteSpace(parameters.Description) && parameters.Description.Trim().Length > 2)
                {
                    var result = await _superMetroidGameService.UpdateGameAsync(gameId, parameters);
                    if (result != null)
                    {
                        return Ok(result);
                    }

                    return NotFound();
                }
            }

            return BadRequest(new { Message = "descrição e nomes precisam ter pelo menos 3 caracteres." });
        }
    }
}
