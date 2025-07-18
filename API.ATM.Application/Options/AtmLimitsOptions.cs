namespace API.ATM.Domain.Options
{
    public sealed class AtmLimitsOptions
    {
        public decimal MaxWithdraw { get; init; }
        public decimal MaxDeposit { get; init; }
    }
}
