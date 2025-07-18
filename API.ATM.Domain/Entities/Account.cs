using API.ATM.Domain.Exceptions;
using API.ATM.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ATM.Domain.Entities
{
    public sealed class Account
    {
        public int Id { get; }
        public CardNumber Card { get; }
        public string PinHash { get; private set; }
        public decimal Balance { get; private set; }

        public Account(int id, CardNumber card, string pinHash, decimal balance)
        {
            Id = id;
            Card = card;
            PinHash = pinHash;
            Balance = balance;
        }

        public void Withdraw(Amount amount)
        {
            if (Balance < amount.Value)
                throw new InsufficientFundsException();

            Balance -= amount.Value;
        }

        public void Deposit(Amount amount) => Balance += amount.Value;

        public void ChangePin(string newHash)
        {
            if (newHash == PinHash)
                throw new SamePinException();
            PinHash = newHash;
        }
    }
}
