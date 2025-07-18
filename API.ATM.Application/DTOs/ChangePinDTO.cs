using System.ComponentModel.DataAnnotations;

namespace API.ATM.Application.DTOs
{
    public class ChangePinDto
    {
        [Required(ErrorMessage = "Card number is required.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Card number must be 16 digits.")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Card number must contain only digits.")]
        public string CardNumber { get; set; } = "";

        [Required(ErrorMessage = "Old PIN is required.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Old PIN must be 4 digits.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "Old PIN must contain only digits.")]
        public string OldPin { get; set; } = "";

        [Required(ErrorMessage = "New PIN is required.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "New PIN must be 4 digits.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "New PIN must contain only digits.")]
        [Compare("OldPin", ErrorMessage = "New PIN cannot be the same as old PIN.")]
        public string NewPin { get; set; } = "";
    }
}
