using API.ATM.Application.DTOs;
using API.ATM.Domain;
using MediatR;

namespace API.ATM.Application.Commands
{
    public sealed record WithdrawCommand(WithdrawDto Request) : IRequest<ApiResponse<Unit>>;
}
