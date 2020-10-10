using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using MoneyApp.Business;
using MoneyApp.Interfaces;
using MoneyApp.Models;
using NUnit.Framework;

namespace MoneyAppTest
{
    public class Tests
    {
        private IMoneyCalculator _moneyCalculator;
        [SetUp]
        public void Setup()
        {
            _moneyCalculator = new MoneyCalculator();
        }

        [Test]
        public void TestMoneyCalculatorSumPerCurrencySingleMoneyObjectReturnsCorrectAmountAndCurrency()
        {
            //Arrange
            IMoney[]  monies = {new Money{Amount=10,Currency="GBP"}};

            //Act
            IEnumerable<IMoney> returnedMoney = _moneyCalculator.SumPerCurrency(monies);

            //Assert
            foreach (var money in returnedMoney)
            {
                Assert.AreEqual(10, money.Amount);
                Assert.AreEqual("GBP", money.Currency);
            }
        }

        [Test]
        public void TestMoneyCalculatorSumPerCurrencyMultipleMoneyObjectsOfSameCurrencyReturnCorrectCurrencyAndValue()
        {
            //Arrange
            IMoney[] monies =
            {
                new Money {Amount = 10, Currency = "GBP"},
                new Money {Amount = 20, Currency = "GBP"},
                new Money {Amount = 50, Currency = "GBP"}
            };

            //Act
            IEnumerable<IMoney> returnedMoney = _moneyCalculator.SumPerCurrency(monies);

            //Assert
            foreach (var money in returnedMoney)
            {
                Assert.AreEqual(80, money.Amount);
                Assert.AreEqual("GBP", money.Currency);
            }
        }

        [Test]
        public void TestMoneyCalculatorSumPerCurrencyMultipleMoneyObjectsOfTwoDifferentCurrenciesReturnCorrectCurrencyAndValue()
        {
            //Arrange
            IMoney[] monies =
            {
                new Money {Amount = 10, Currency = "GBP"},
                new Money {Amount = 20, Currency = "GBP"},
                new Money {Amount = 50, Currency = "EUR"}
            };

            //Act
            IEnumerable<IMoney> returnedMoney = _moneyCalculator.SumPerCurrency(monies);

            //Assert
            foreach (var money in returnedMoney)
            {
                if (money.Currency == "GBP")
                {
                    Assert.AreEqual(new Money{Amount = 30, Currency = "GBP"}, money);
                }

                if (money.Currency == "EUR")
                {
                    Assert.AreEqual(new Money { Amount = 50, Currency = "EUR" }, money);
                }
            }
        }

        [Test]
        public void TestMoneyCalculatorSumPerCurrencyDifferentMoneyObjectsAreNotEqualToProveEqualsOverrideWorked()
        {
            //Arrange
            IMoney[] monies =
            {
                new Money {Amount = 10, Currency = "GBP"},
                new Money {Amount = 20, Currency = "GBP"},
                new Money {Amount = 50, Currency = "EUR"},
                new Money {Amount = 300, Currency = "USD"}
            };

            //Act
            IEnumerable<IMoney> returnedMoney = _moneyCalculator.SumPerCurrency(monies);

            //Assert
            foreach (var money in returnedMoney)
            {
                Assert.AreNotEqual(new Money { Amount = 50, Currency = "INR" }, money);
                Assert.AreNotEqual(new Money { Amount = 30, Currency = "EUR" }, money);
            }
        }

        [Test]
        public void TestMoneyCalculatorMaxReturnsValueWhenOneMoneySubmitted()
        {
            //Arrange
            IMoney expectedMoney = new Money {Amount = 20, Currency = "GBP"};
            IMoney[] monies =
            {
               expectedMoney
            };

            //Act
            IMoney returnedMoney = _moneyCalculator.Max(monies);

            //Assert
            Assert.AreEqual(expectedMoney, returnedMoney);
        }

        [Test]
        public void TestMoneyCalculatorMaxReturnsValueWhenTwoMoneySubmitted()
        {
            //Arrange
            IMoney expectedMoney = new Money { Amount = 20, Currency = "GBP" };
            IMoney[] monies =
            {
                expectedMoney,
                new Money{Amount = 19, Currency = "GBP"}
            };

            //Act
            IMoney returnedMoney = _moneyCalculator.Max(monies);

            //Assert
            Assert.AreEqual(expectedMoney, returnedMoney);
        }

        [Test]
        public void TestMoneyCalculatorMaxReturnsValueWhenMultipleMoneySubmitted()
        {
            //Arrange
            IMoney expectedMoney = new Money { Amount = 10000, Currency = "GBP" };
            IMoney[] monies =
            {
                expectedMoney,
                new Money{Amount = 19, Currency = "GBP"},
                new Money{Amount = -999999, Currency = "GBP"},
                new Money{Amount = 0, Currency = "GBP"},
                new Money{Amount = 9999, Currency = "GBP"}
            };

            //Act
            IMoney returnedMoney = _moneyCalculator.Max(monies);

            //Assert
            Assert.AreEqual(expectedMoney, returnedMoney);
        }

        [Test]
        public void TestMoneyCalculatorMaxThrowsExceptionWhenDifferentCurrenciesSubmitted()
        {
            //Arrange
            IMoney[] monies =
            {
                new Money{Amount = 19, Currency = "GBP"},
                new Money{Amount = -999999, Currency = "GBP"},
                new Money{Amount = 0, Currency = "GBP"},
                new Money{Amount = 9999, Currency = "USD"}
            };

            //Act
            //Assert
            var ex = Assert.Throws<ArgumentException>(() => _moneyCalculator.Max(monies));
            Assert.That(ex.Message, Is.EqualTo("All monies are not in the same currency."));
        }
    }
}