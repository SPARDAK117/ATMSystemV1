namespace API.ATM.Application.DTOs
{
    public class WithdrawDto
    {
        public string CardNumber { get; set; } = "";
        public string Pin { get; set; } = "";
        public decimal Amount { get; set; }
    }
}
