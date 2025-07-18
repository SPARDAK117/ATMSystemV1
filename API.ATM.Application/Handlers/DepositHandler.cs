using API.ATM.Application.Commands;
using API.ATM.Application.Contracts;
using API.ATM.Domain;
using MediatR;

namespace API.ATM.Application.Handlers
{
    public class DepositHandler : IRequestHandler<DepositCommand, ApiResponse<Unit>>
    {
        private readonly IAtmRepository _atmRepository;

        public DepositHandler(IAtmRepository atmRepository)
        {
            _atmRepository = atmRepository;
        }

        public async Task<ApiResponse<Unit>> Handle(DepositCommand command, CancellationToken cancellationToken)
        {
            return await _atmRepository.DepositAsync(command, cancellationToken);
        }
    }

}
