using API.ATM.Application.DTOs;
using API.ATM.Domain;
using MediatR;

namespace API.ATM.Application.Queries
{
    public sealed record BalanceQuery(BalanceRequest Request) : IRequest<ApiResponse<BalanceResponse>>;
}
