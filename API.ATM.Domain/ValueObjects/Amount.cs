using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Domain.ValueObjects
{
    public sealed record Amount
    {
        public decimal Value { get; }

        public Amount(decimal value)
        {
            if (value <= 0) throw new ArgumentException("Amount must be positive.");
            Value = value;
        }
    }
}
