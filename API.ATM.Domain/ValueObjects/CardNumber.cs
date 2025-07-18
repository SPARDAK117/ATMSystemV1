using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Domain.ValueObjects
{
    public sealed record CardNumber
    {
        public string Value { get; }

        public CardNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 16 || !value.All(char.IsDigit))
                throw new ArgumentException("CardNumber must be 16 digits.");
            Value = value;
        }

        public string Last4() => Value[^4..];
    }
}
