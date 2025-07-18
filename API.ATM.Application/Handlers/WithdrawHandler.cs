using API.ATM.Application.Commands;
using API.ATM.Application.Contracts;
using API.ATM.Application.DTOs;
using API.ATM.Domain;
using API.ATM.Shared;
using MediatR;

namespace API.ATM.Application.Handlers
{
    public class WithdrawHandler : IRequestHandler<WithdrawCommand, ApiResponse<Unit>>
    {
        private readonly IAtmRepository AtmRepository;
        private readonly ILoginRepository LoginRepository;

        public WithdrawHandler(IAtmRepository AtmService, ILoginRepository loginRepository)
        {
            AtmRepository = AtmService;
            LoginRepository = loginRepository;
        }

        public async Task<ApiResponse<Unit>> Handle(WithdrawCommand Command, CancellationToken cancellationToken)
        {
            var UserIsValid = await LoginRepository.ValidateCardAndPinAsync(Command.Request.CardNumber, Command.Request.Pin);

            if(UserIsValid)
            {
                ApiResponse<Unit> Result = await AtmRepository.WithdrawAsync(Command, cancellationToken);

                return Result;
            }

            return ApiResponse<Unit>.Fail(ErrorCodes.InvalidCredentials,"The credentials are invalid");
        }
    }
}
