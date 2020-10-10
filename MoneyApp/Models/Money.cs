using System;
using System.Collections.Generic;
using System.Text;
using MoneyApp.Interfaces;

namespace MoneyApp.Models
{
    public class Money : IMoney
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public override bool Equals(object obj)
        {
            return Equals(obj as Money);
        }

        public bool Equals(Money obj)
        {
            return obj != null && obj.Amount == Amount && obj.Currency == Currency;
        }

    }
}
