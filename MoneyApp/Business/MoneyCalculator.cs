using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoneyApp.Interfaces;
using MoneyApp.Models;

namespace MoneyApp.Business
{
    public class MoneyCalculator : IMoneyCalculator
    {
        public IMoney Max(IEnumerable<IMoney> monies)
        {
            var currency = monies.First().Currency;
            
            return monies.All(x => x.Currency == currency) ? monies.OrderByDescending(i => i.Amount).First() : throw new ArgumentException("All monies are not in the same currency.");
        }

        public IEnumerable<IMoney> SumPerCurrency(IEnumerable<IMoney> monies)
        {
           Dictionary<string, decimal> calculatedValues = new Dictionary<string, decimal>();
           foreach (var money in monies)
           {
               if (calculatedValues.ContainsKey(money.Currency))
               {
                   calculatedValues[money.Currency] += money.Amount;
               }
               else
               {
                   calculatedValues.Add(money.Currency, money.Amount);
               }
           }

           List<IMoney> aggregatedMonies = new List<IMoney>();
           foreach (var item in calculatedValues)
           {
               aggregatedMonies.Add(new Money{Currency = item.Key, Amount = item.Value});
           }

           return aggregatedMonies;
        }
    }
}
