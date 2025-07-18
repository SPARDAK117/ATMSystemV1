using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Shared.Validations
{
    public static class RegexPatterns
    {
        public const string CardNumber = "^\\d{16}$";
        public const string Pin = "^\\d{4,6}$";
        public const string AccountNumber = "^[A-Za-z0-9]{6,20}$";
        public const string AccountOrCard = "^(?:\\d{16}|[A-Za-z0-9]{6,20})$";
    }
}
