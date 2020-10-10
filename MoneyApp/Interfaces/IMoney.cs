using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyApp.Interfaces
{
    /// <summary>
    /// An amount of money in a particular currency.
    /// </summary>
    public interface IMoney
    {
        /// <summary>
        /// The amount of money this instance represents.
        /// </summary>
        decimal Amount { get; }

        /// <summary>
        /// The ISO currency code of this instance.
        /// </summary>
        string Currency { get; }
    }
}
