using API.ATM.Application;
using API.ATM.Application.Commands;
using API.ATM.Application.DTOs;
using API.ATM.Application.Queries;
using API.ATM.Domain;
using API.ATM.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.ATM.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retira una cantidad del cajero (máximo $8,000)
        /// </summary>
        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawRequest request)
        {
            if (!ModelState.IsValid)
                return Ok(ApiEnvelope.Fail(ErrorCodes.Validation, "Invalid input", ModelState));

            string cardNumber = User.FindFirst("cardNumber")?.Value ?? "";
            string pin = User.FindFirst("pin")?.Value ?? "";

            if (string.IsNullOrWhiteSpace(cardNumber))
                return Ok(ApiEnvelope.Fail(ErrorCodes.InvalidCredentials,"Invalid credentials"));

            WithdrawDto Dto = new()
            {
                CardNumber = cardNumber,
                Pin = pin,
                Amount = request.Amount
            };

            var result = await _mediator.Send(new WithdrawCommand(Dto));
            return Ok(result);
        }

        /// <summary>
        /// Deposita una cantidad al cajero (máximo $15,000)
        /// </summary>
        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositRequest request)
        {
            if (!ModelState.IsValid)
                return Ok(ApiEnvelope.Fail(ErrorCodes.Validation, "Invalid input", ModelState));

            string? cardNumber = User.FindFirst("cardNumber")?.Value;
            string? accountNumber = User.FindFirst("accountNumber")?.Value;

            string? accountOrCard = !string.IsNullOrWhiteSpace(accountNumber)
                                    ? accountNumber
                                    : cardNumber;

            if (string.IsNullOrWhiteSpace(accountOrCard))
                return Ok(ApiEnvelope.Fail(ErrorCodes.InvalidCredentials, "No account or card available"));

            DepositDto Dto = new()
            {
                AccountOrCard = accountOrCard,
                Amount = request.Amount,
                
            };
            var command = new DepositCommand(Dto);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        /// <summary>
        /// Consulta el saldo actual de una cuenta
        /// </summary>
        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance()
        {
            string cardNumber = User.FindFirst("cardNumber")?.Value ?? "";
            string pin = User.FindFirst("pin")?.Value ?? "";

            if (string.IsNullOrWhiteSpace(cardNumber) || string.IsNullOrWhiteSpace(pin))
                return Ok(ApiEnvelope.Fail(ErrorCodes.InvalidCredentials, "Invalid credentials"));

            BalanceRequest request = new()
            {
                CardNumber = cardNumber,
                Pin = pin
            };

            var result = await _mediator.Send(new BalanceQuery(request));
            return Ok(result);
        }
    }
}
