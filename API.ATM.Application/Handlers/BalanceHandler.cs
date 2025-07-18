using API.ATM.Application.Contracts;
using API.ATM.Application.DTOs;
using API.ATM.Application.Queries;
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
    public class BalanceHandler : IRequestHandler<BalanceQuery, ApiResponse<BalanceResponse>>
    {
        private readonly IAtmRepository AtmRepository;
        private readonly ILoginRepository LoginRepository;

        public BalanceHandler(IAtmRepository atmRepository,ILoginRepository loginRepository)
        {
            AtmRepository = atmRepository;
            LoginRepository = loginRepository;
        }

        public async Task<ApiResponse<BalanceResponse>> Handle(BalanceQuery Query, CancellationToken cancellationToken)
        {
            var UserIsValid = await LoginRepository.ValidateCardAndPinAsync(Query.Request.CardNumber, Query.Request.Pin);

            if (UserIsValid)
            {
                ApiResponse<BalanceResponse> Result = await AtmRepository.GetBalanceAsync(Query, cancellationToken);

                return Result;
            }

            return ApiResponse<BalanceResponse>.Fail(ErrorCodes.InvalidCredentials, "Invalid CardNumber or PIN");
        }
    }
}
