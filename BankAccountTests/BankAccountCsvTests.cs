using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Xunit;
using BankAccountApp;

namespace BankAccountTests
{
    public class BankAccountCsvTests
    {
        public static IEnumerable<object[]> GetTestData()
        {
            string filePath = Path.Combine(
                AppContext.BaseDirectory,
                "BankAccountTestData.csv");

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(
                reader, CultureInfo.InvariantCulture))
            {
                foreach (var record in csv.GetRecords<TestData>())
                {
                    yield return new object[]
                    {
                        record.CustomerName,
                        record.InitialBalance,
                        record.DebitAmount,
                        record.ExpectedBalance
                    };
                }
            }
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Debit_CsvData_UpdatesBalance(
            string customerName,
            decimal initialBalance,
            decimal debitAmount,
            string expectedBalance)
        {
            // Arrange
            var account = new BankAccount(
                customerName, initialBalance);

            // Act & Assert
            if (expectedBalance == "Insufficient funds")
            {
                Assert.Throws<InvalidOperationException>(
                    () => account.Debit(debitAmount));
            }
            else
            {
                account.Debit(debitAmount);

                Assert.Equal(
                    decimal.Parse(expectedBalance),
                    account.Balance);
            }
        }

        public class TestData
        {
            public string CustomerName { get; set; }
            public decimal InitialBalance { get; set; }
            public decimal DebitAmount { get; set; }
            public string ExpectedBalance { get; set; }
        }
    }
}
