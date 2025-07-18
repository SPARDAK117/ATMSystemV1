using API.ATM.Application.Commands;
using API.ATM.Application.Contracts;
using API.ATM.Application.DTOs;
using API.ATM.Domain;
using API.ATM.Shared;
using MediatR;

namespace API.ATM.Application.Handlers
{
    public class ChangePinHandler : IRequestHandler<ChangePinCommand, ApiResponse<Unit>>
    {
        private readonly IAtmRepository AtmRepository;
        private readonly ILoginRepository LoginRepository;

        public ChangePinHandler(IAtmRepository atmRepository, ILoginRepository loginRepository)
        {
            AtmRepository = atmRepository; 
            LoginRepository = loginRepository;
        }

        public async Task<ApiResponse<Unit>> Handle(ChangePinCommand Command, CancellationToken cancellationToken)
        {
            var UserIsValid = await LoginRepository.ValidateCardAndPinAsync(Command.Request.CardNumber, Command.Request.OldPin);

            if (UserIsValid)
            {
                ApiResponse<Unit> Result = await AtmRepository.ChangePinAsync(Command, cancellationToken);
                return Result;
            }

            return ApiResponse<Unit>.Fail(ErrorCodes.InvalidCredentials, "Invalid CardNumber or PIN");
        }
    }
}
