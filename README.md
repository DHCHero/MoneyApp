Provide a working solution containing implementations of the following interfaces, with tests demonstrating their correctness.
Use whatever tools and .NET version you have available.
Please submit your code through a public git repository, e.g. GitHub.

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
    /// <summary>
    /// Some fun things to do with money.
    /// </summary>
    public interface IMoneyCalculator
    {
        /// <summary>
        /// Find the largest amount of money.
        /// </summary>
        /// <returns>The <see cref="IMoney"/> instance having the largest amount.</returns>
        /// <exception cref="ArgumentException">All monies are not in the same currency.</exception>
        IMoney Max(IEnumerable<IMoney> monies);

        /// <summary>
        /// Return a <see cref="IMoney"/> per currency with the sum of all monies of the same currency.
        /// </summary>
        /// <example>{GBP10, GBP20, GBP50} => {GBP80}</example>
        /// <example>{GBP10, GBP20, EUR50} => {GBP30, EUR50}</example>
        /// <example>{GBP10, USD20, EUR50} => {GBP10, USD20, EUR50}</example>
        IEnumerable<IMoney> SumPerCurrency(IEnumerable<IMoney> monies);
    }
