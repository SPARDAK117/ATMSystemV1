using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Application.DTOs.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Card number is required.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Card number must be 16 digits.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must contain only digits.")]
        public string CardNumber { get; set; } = "";

        [Required(ErrorMessage = "PIN is required.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "PIN must be 4 digits.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "PIN must contain only digits.")]
        public string Pin { get; set; } = "";
    }
}
