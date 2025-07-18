using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Domain.ValueObjects
{
    public sealed record Pin
    {
        public string Value { get; }

        public Pin(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 4 || !value.All(char.IsDigit))
                throw new ArgumentException("PIN must be 4 digits.");
            Value = value;
        }
    }
}
