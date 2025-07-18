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
            var salt = Guid.NewGuid();
            using var sha256 = SHA256.Create();
            var combined = Encoding.UTF8.GetBytes(pin + salt.ToString());
            var hash = sha256.ComputeHash(combined);
            return (salt, hash);
        }
    }

}
