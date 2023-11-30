using CryptoScrapper.DAL.Interfaces;
using CryptoScrapperAPI_PAD.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoScrapperAPI_PAD.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateUserCommands.CommandResponseUser), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> CreateUser([FromBody] CreateUserCommands.CommandUser cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(LoginCommands.LoginCommandResponse), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), 400)]
        public async Task<JsonResult> Login([FromBody] LoginCommands.LoginCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return new JsonResult(result);
        }

    }
}
