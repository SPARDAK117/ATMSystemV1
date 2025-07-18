using API.ATM.Shared.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Application.DTOs
{
    public class DepositDto
    {
        [Required(ErrorMessage = "Account number or Card number is required.")]
        [RegularExpression(@"^(\d{10}|\d{16})$", ErrorMessage = "Invalid Account or Card Number format. Must be a 10-digit account number or a 16-digit card number.")]
        public string AccountOrCard { get; set; } = "";

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, 15000.00, ErrorMessage = "Deposit amount must be between $0.01 and $15,000.00.")]
        public decimal Amount { get; set; }
    }
}
