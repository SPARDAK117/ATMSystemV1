using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Domain.Entities
{
    public class CardPinData
    {
        public Guid PinSalt { get; set; }
        public byte[] PinHash { get; set; } = Array.Empty<byte>();
        public bool IsActive { get; set; }
    }
}
