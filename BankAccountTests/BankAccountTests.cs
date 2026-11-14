using Xunit;
using BankAccountApp;
using System;

namespace BankAccountTests
{
    public class BankAccountTests
    {
        // Test Debit with valid amounts
        [Theory]
        [InlineData("John Doe", 1000, 200, 800)]
        [InlineData("Jane Smith", 500, 100, 400)]
        [InlineData("Alice Johnson", 300, 50, 250)]
        [InlineData("Boundary", 100, 100, 0)]
        public void Debit_ValidAmount_UpdatesBalance(
            string customerName,
            decimal initialBalance,
            decimal debitAmount,
            decimal expectedBalance)
        {
            // Arrange
            var account = new BankAccount(
                customerName, initialBalance);

            // Act
            account.Debit(debitAmount);

            // Assert
            Assert.Equal(expectedBalance, account.Balance);
        }

        // Test Debit with insufficient funds
        [Theory]
        [InlineData("Bob Brown", 100, 150)]
        [InlineData("Boundary", 100, 101)]
        public void Debit_InsufficientFunds_ThrowsException(
            string customerName,
            decimal initialBalance,
            decimal debitAmount)
        {
            var account = new BankAccount(
                customerName, initialBalance);

            Assert.Throws<InvalidOperationException>(
                () => account.Debit(debitAmount));
        }

        // Test Credit
        [Theory]
        [InlineData("John Doe", 1000, 200, 1200)]
        [InlineData("Jane Smith", 500, 100, 600)]
        [InlineData("Alice Johnson", 300, 50, 350)]
        [InlineData("Boundary", 100, 0, 100)]
        public void Credit_ValidAmount_UpdatesBalance(
            string customerName,
            decimal initialBalance,
            decimal creditAmount,
            decimal expectedBalance)
        {
            var account = new BankAccount(
                customerName, initialBalance);

            account.Credit(creditAmount);

            Assert.Equal(expectedBalance, account.Balance);
        }

        // Test Withdraw
        [Theory]
        [InlineData("John Doe", 1000, 200, 800)]
        [InlineData("Jane Smith", 500, 100, 400)]
        [InlineData("Alice Johnson", 300, 50, 250)]
        [InlineData("Boundary", 100, 100, 0)]
        public void Withdraw_ValidAmount_UpdatesBalance(
            string customerName,
            decimal initialBalance,
            decimal withdrawAmount,
            decimal expectedBalance)
        {
            var account = new BankAccount(
                customerName, initialBalance);

            account.Withdraw(withdrawAmount);

            Assert.Equal(expectedBalance, account.Balance);
        }

        // Test Withdraw with insufficient funds
        [Fact]
        public void Withdraw_InsufficientFunds_ThrowsException()
        {
            var account = new BankAccount("Test", 100);

            Assert.Throws<InvalidOperationException>(
                () => account.Withdraw(150));
        }
    }
}
