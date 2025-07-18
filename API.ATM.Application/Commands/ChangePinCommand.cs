using API.ATM.Application.DTOs;
using API.ATM.Domain;
using MediatR;

namespace API.ATM.Application.Commands
{
    public sealed record ChangePinCommand(ChangePinDTO Request) : IRequest<ApiResponse<Unit>>;
}
