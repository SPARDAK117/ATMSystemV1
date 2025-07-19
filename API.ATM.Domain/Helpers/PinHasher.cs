using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Domain.Helpers
{
    public static class PinHasher
    {
        public static (Guid Salt, byte[] Hash) HashPin(string pin)
        {
            Guid salt = Guid.NewGuid();
            using SHA512 sha512 = SHA512.Create();
            byte[] combined = Encoding.UTF8.GetBytes(pin + salt.ToString());
            byte[] hash = sha512.ComputeHash(combined);
            return (salt, hash);
        }
    }

}
