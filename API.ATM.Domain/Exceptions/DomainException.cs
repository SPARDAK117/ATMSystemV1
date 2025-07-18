using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message) : base(message) { }
    }

    public sealed class InsufficientFundsException : DomainException
    {
        public InsufficientFundsException() : base("Insufficient funds.") { }
    }

    public sealed class SamePinException : DomainException
    {
        public SamePinException() : base("New PIN must differ from current PIN.") { }
    }
}
