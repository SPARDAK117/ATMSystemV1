using API.ATM.Shared.Validations;
using System.ComponentModel.DataAnnotations;

namespace API.ATM.Application.DTOs
{
    public sealed class DepositRequest
    {
        [Required, Range(1, 14999.99)] public decimal Amount { get; set; }
    }

}
