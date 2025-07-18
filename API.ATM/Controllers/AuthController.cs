using API.ATM.Application;
using API.ATM.Application.Commands;
using API.ATM.Application.DTOs.Auth;
using API.ATM.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.ATM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator Mediator;
        public AuthController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return Ok(ApiEnvelope.Fail(ErrorCodes.Validation, "Invalid input", ModelState));

            var command = new LoginCommand(request.CardNumber, request.Pin);

            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
