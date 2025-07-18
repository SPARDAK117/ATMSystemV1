using System.ComponentModel.DataAnnotations;

namespace API.ATM.Application.DTOs
{
    public class WithdrawDto
    {
        [Required(ErrorMessage = "Card number is required.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Card number must be 16 digits.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must contain only digits.")]
        public string CardNumber { get; set; } = "";

        [Required(ErrorMessage = "PIN is required.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "PIN must be 4 digits.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "PIN must contain only digits.")]
        public string Pin { get; set; } = "";

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, 8000.00, ErrorMessage = "Withdrawal amount must be between $0.01 and $8,000.00.")]
        public decimal Amount { get; set; }
    }
}
