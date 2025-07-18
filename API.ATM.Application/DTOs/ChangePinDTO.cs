namespace API.ATM.Application.DTOs
{
    public class ChangePinDTO
    {
        public string CardNumber { get; set; } = "";
        public string OldPin { get; set; } = "";
        public string NewPin { get; set; } = "";
    }
}
