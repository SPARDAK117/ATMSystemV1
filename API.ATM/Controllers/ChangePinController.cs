using API.ATM.Application;
using API.ATM.Application.Commands;
using API.ATM.Application.DTOs;
using API.ATM.Application.DTOs.Auth;
using API.ATM.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.ATM.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChangePinController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChangePinController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> ChangePin([FromBody] ChangePinRequest request)
        {
            if (!ModelState.IsValid)
                return Ok(ApiEnvelope.Fail(ErrorCodes.Validation, "Invalid input", ModelState));

            string cardNumber = User.FindFirst("cardNumber")?.Value ?? "";

            ChangePinDto Dto = new()
            {
                CardNumber = cardNumber,
                NewPin = request.NewPin,
                OldPin = request.OldPin,
            };

            var result = await _mediator.Send(new ChangePinCommand(Dto));
            return Ok(result);
        }
    }
}
