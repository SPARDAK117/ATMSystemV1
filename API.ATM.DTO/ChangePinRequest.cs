using API.ATM.Application.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.DTO
{
    public sealed class ChangePinRequest
    {
        [Required, RegularExpression(RegexPatterns.CardNumber)] public string CardNumber { get; set; } = "";
        [Required, RegularExpression(RegexPatterns.Pin)] public string OldPin { get; set; } = "";
        [Required, RegularExpression(RegexPatterns.Pin)] public string NewPin { get; set; } = "";
    }
}
