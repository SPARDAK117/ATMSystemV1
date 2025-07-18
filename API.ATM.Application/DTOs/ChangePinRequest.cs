using API.ATM.Shared.Validations;
using System.ComponentModel.DataAnnotations;

namespace API.ATM.Application.DTOs
{
    public sealed class ChangePinRequest
    {
        [Required, RegularExpression(RegexPatterns.Pin)] public string OldPin { get; set; } = "";
        [Required, RegularExpression(RegexPatterns.Pin)] public string NewPin { get; set; } = "";
    }
}
