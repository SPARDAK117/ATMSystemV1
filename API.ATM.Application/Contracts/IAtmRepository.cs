using API.ATM.Application.Commands;
using API.ATM.Application.DTOs;
using API.ATM.Application.Queries;
using API.ATM.Domain;
using MediatR;

namespace API.ATM.Application.Contracts
{
    public interface IAtmRepository
    {
        Task<ApiResponse<Unit>> WithdrawAsync(WithdrawCommand Command, CancellationToken cancellationToken);
        Task<ApiResponse<Unit>> ChangePinAsync(ChangePinCommand Command, CancellationToken cancellationToken);
        Task<ApiResponse<Unit>> DepositAsync(DepositCommand Command, CancellationToken cancellationToken);
        Task<ApiResponse<BalanceResponse>> GetBalanceAsync(BalanceQuery Query, CancellationToken cancellationToken);

    }
}
