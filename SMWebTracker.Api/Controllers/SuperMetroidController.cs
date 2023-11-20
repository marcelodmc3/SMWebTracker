using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMWebTracker.Domain.Dtos;
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
    }
}
