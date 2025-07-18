using API.ATM.Application.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.DTO
{
    public sealed class DepositRequest
    {
        [Required, RegularExpression(RegexPatterns.AccountOrCard)] public string AccountOrCard { get; set; } = "";
        [Required, Range(1, 14999.99)] public decimal Amount { get; set; }
    }

}
