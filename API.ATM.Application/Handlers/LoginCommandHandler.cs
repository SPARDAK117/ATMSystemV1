using API.ATM.Application.Commands;
using API.ATM.Application.Contracts;
using API.ATM.Application.DTOs.Auth;
using API.ATM.Domain;
using API.ATM.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Application.Handlers
{
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<LoginResponse>>
    {
        private readonly ILoginRepository LoginRepository;
        private readonly IJwtTokenGenerator TokenGenerator;

        public LoginCommandHandler(ILoginRepository LoginRepo, IJwtTokenGenerator tokenGenerator)
        {
            LoginRepository = LoginRepo;
            TokenGenerator = tokenGenerator;
        }

        public async Task<ApiResponse<LoginResponse>> Handle(LoginCommand Request, CancellationToken cancellationToken)
        {
            bool isValid = await LoginRepository.ValidateCardAndPinAsync(Request.CardNumber, Request.Pin);
            if (!isValid)
                return ApiResponse<LoginResponse>.Fail(ErrorCodes.InvalidCredentials, "Invalid credentials");

            string? AccountNumber = await LoginRepository.GetAccountNumberByCardAsync(Request.CardNumber);
            string? token = TokenGenerator.GenerateToken(Request.CardNumber, Request.Pin, AccountNumber);

            return ApiResponse<LoginResponse>.Ok(new LoginResponse { Token = token });
        }
    }

}
