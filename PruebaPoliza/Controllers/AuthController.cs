using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.EventHandlers.Commands;
using Service.EventHandlers.Responses;

namespace API.Controllers
{
    /// <summary>
    /// Auth controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        public AuthController(IMediator mediator)
            => _mediator = mediator;

        /// <summary>
        /// Genera un token para los datos datos
        /// </summary>
        /// <remarks>
        /// Ejemplo request:
        ///
        ///     POST /login
        ///     {
        ///        "username": "darl.8910",
        ///        "password": "123.abc"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestCommand login)
            => Ok(await _mediator.Send(login));

        /// <summary>
        /// Crea un usuario en la base de datos
        /// </summary>
        /// <remarks>
        /// Ejemplo request:
        ///
        ///     POST /register
        ///     {
        ///         "userName": "darl.8910",
        ///         "password": "123.abc",
        ///         "fullName": "Roberto Cerquera",
        ///         "email": "rcerque@gmail.com"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<UserInfoResult>> RegisterAsync([FromBody] UserCreateCommand req)
            => Ok(await _mediator.Send(req));
    }
}
