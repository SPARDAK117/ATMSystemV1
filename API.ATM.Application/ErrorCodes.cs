
namespace API.ATM.Shared
{
    public static class ErrorCodes
    {
        public const int Success = 0;         
        public const int AccountNotFound = 1;
        public const int InvalidCredentials = 2;
        public const int Validation = 3; 
        public const int LimitExceededWithdraw = 4;
        public const int LimitExceededDeposit = 4; 
        public const int InsufficientFunds = 5; 
        public const int PinSameAsOld = 6;
        public const int DataAccess = 900; 
        public const int Unknown = 900;  
    }
}
