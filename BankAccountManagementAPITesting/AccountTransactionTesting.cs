using BankAccountManagementAPI.Controllers;
using BankAccountManagementAPI.Models;
using BankAccountManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BankAccountManagementAPITesting
{
    public class AccountTransactionTesting
    {
        private readonly AccountTransactionController _accountTransactionController;

        public AccountTransactionTesting()
        {
            _accountTransactionController = new AccountTransactionController();
        }

        [Fact(DisplayName = "Operación de depósito")]
        public void Deposit_Ok()
        {
            // Arrange
            var testAccount = new UserAccount()
            {
                Id = 1,
                AccountNumber = 1224,
                Name = "Test",
                MiddleName = "Test",
                LastName = "Test",
                SecontLastName = "Test",
                Currency = UserAccount.ECurrency.NIO,
                Balance = 1000.00m,
            };

            AccountsDataBase.Current.UserAccounts.Add(testAccount);

            decimal depositAmount = 500.00m;

            var newBalance = depositAmount + testAccount.Balance;

            var newTransaction = new AccountTransactionInsert()
            {
                AccountNumber = 1224,
                Amount = depositAmount,
            };

            // Act
            var result = (CreatedAtActionResult)_accountTransactionController.CreateDeposit(newTransaction);
            var deposit = Assert.IsType<AccountTransaction>(result.Value);

            // Assert
            Assert.NotNull(deposit);
            Assert.Equal(deposit.Balance, newBalance);
        }
    }
}
