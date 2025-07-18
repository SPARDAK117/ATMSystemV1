using API.ATM.Application.Commands;
using API.ATM.Application.Contracts;
using API.ATM.Application.DTOs;
using API.ATM.Application.Queries;
using API.ATM.Domain;
using API.ATM.Infrastructure.Repositories.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Infrastructure
{
    public sealed class AtmRepository : IAtmRepository
    {
        private readonly WithdrawCommandExecutor Withdraw;
        private readonly ChangePinCommandExecutor ChangePin;
        private readonly DepositCommandExecutor Deposit;
        private readonly BalanceQueryExecutor Balance;

        public AtmRepository(
            WithdrawCommandExecutor withdraw,
            ChangePinCommandExecutor changePin,
            DepositCommandExecutor deposit,
            BalanceQueryExecutor balance)
        {
            Withdraw = withdraw;
            ChangePin = changePin;
            Deposit = deposit;
            Balance = balance;
        }

        public async Task<ApiResponse<Unit>> WithdrawAsync(WithdrawCommand Command, CancellationToken cancellationToken)
            => await Withdraw.ExecuteAsync(Command, cancellationToken);

        public async Task<ApiResponse<Unit>> ChangePinAsync(ChangePinCommand Command, CancellationToken cancellationToken)
            => await ChangePin.ExecuteAsync(Command, cancellationToken);

        public async Task<ApiResponse<Unit>> DepositAsync(DepositCommand Command, CancellationToken cancellationToken)
            => await Deposit.ExecuteAsync(Command, cancellationToken);

        public async Task<ApiResponse<BalanceResponse>> GetBalanceAsync(BalanceQuery Query, CancellationToken cancellationToken)
            => await Balance.ExecuteAsync(Query, cancellationToken);
    }
}
