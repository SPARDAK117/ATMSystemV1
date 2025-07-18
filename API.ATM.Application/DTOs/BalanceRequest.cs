using API.ATM.Shared.Validations;
using System.ComponentModel.DataAnnotations;

namespace API.ATM.Application.DTOs
{
    public sealed class BalanceRequest
    {
        [Required, RegularExpression(RegexPatterns.CardNumber)] public string CardNumber { get; set; } = "";
        [Required, RegularExpression(RegexPatterns.Pin)] public string Pin { get; set; } = "";
    }
}
