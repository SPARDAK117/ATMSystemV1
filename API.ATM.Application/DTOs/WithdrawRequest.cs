using API.ATM.Shared.Validations;
using System.ComponentModel.DataAnnotations;

namespace API.ATM.Application.DTOs
{
    public sealed class WithdrawRequest
    {
        [Required, Range(1, 7999.99)] public decimal Amount { get; set; }
    }
}
