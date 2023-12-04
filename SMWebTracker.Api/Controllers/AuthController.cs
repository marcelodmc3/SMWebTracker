using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SMWebTracker.Domain.Dtos;
using SMWebTracker.Domain.Interfaces;
using System.Security.Claims;

namespace SMWebTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody] NewUserModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.CreateUser(userModel, User.FindFirst(ClaimTypes.Email)?.Value);
                    if (result != null)
                    {
                        return Created("create/usuario/", result);
                    }

                    return BadRequest($"O Usuário '{userModel.Login}' já existe.");
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (UnauthorizedAccessException)
            {
                return BadRequest($"Somente Administradores podem criar outros usuários Administradores.");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var userData = await _userService.Login(login);
            if (userData != null)
            {
                return Ok(userData);
            }

            return NotFound("Usuário ou senha inválidos.");
        }

        [HttpGet("ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ping()
        {
            return Ok();
        }
    }
}
