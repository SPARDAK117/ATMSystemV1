using API.ATM.Application.Validations;
using System.ComponentModel.DataAnnotations;

namespace API.ATM.DTO
{
    public sealed class WithdrawRequest
    {
        [Required, RegularExpression(RegexPatterns.CardNumber)] public string CardNumber { get; set; } = "";
        [Required, RegularExpression(RegexPatterns.Pin)] public string Pin { get; set; } = "";
        [Required, Range(1, 7999.99)] public decimal Amount { get; set; }
    }
}
