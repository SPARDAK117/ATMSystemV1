using API.ATM.Application.DTOs.Auth;
using API.ATM.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Application.Commands
{
    public sealed class LoginCommand : IRequest<ApiResponse<LoginResponse>>
    {
        public string CardNumber { get; init; }
        public string Pin { get; init; }

        public LoginCommand(string cardNumber, string pin)
        {
            CardNumber = cardNumber;
            Pin = pin;
        }
    }
}
