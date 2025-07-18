using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Application.Contracts
{
    public interface ILoginRepository
    {
        Task<bool> ValidateCardAndPinAsync(string CardNumber, string Pin);
        Task<string?> GetAccountNumberByCardAsync(string CardNumber);
    }
}
