using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Application.Contracts
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string CardNumber, string Pin, string? AccountNumber);
    }
}
