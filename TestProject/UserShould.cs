using SimpleWebAplication.Models;
using System;
using Xunit;

namespace TestProject
{
    public class UserShould
    {
        [Fact]
        public void BalanceIsIncreased_WhenDepositIsPositiveValue()
        {
            var user = new User();
            var oldBalance = user.AccoutBalance;

            user.IncreaseBalance(1);

            Assert.Equal(oldBalance + 1, user.AccoutBalance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ArgumentException_WhenDepositIsZeroOrNegative(decimal amount)
        {
            var user = new User();
            var oldBalance = user.AccoutBalance;

            Assert.Throws<ArgumentException>(() => {
                user.IncreaseBalance(amount);
            });
        }

        [Fact]
        public void BalanceIsReduced_WhenPay()
        {
            var user = new User();
            user.IncreaseBalance(1);
            var oldBalance = user.AccoutBalance;
            user.DecreaseBalance(1);

            Assert.Equal(oldBalance - 1, user.AccoutBalance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(2)]
        public void ArgumentException_WhenDecraseValueIsLowerThanZeroOrOverflowAccountValue(decimal amount)
        {
            var user = new User();

            user.IncreaseBalance(1);
            var oldBalance = user.AccoutBalance;

            Assert.Throws<ArgumentException>(() => {
                user.DecreaseBalance(amount);
            });
        }
    }
}