using BankAccountManagementAPI.Controllers;
using BankAccountManagementAPI.Models;
using BankAccountManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BankAccountManagementAPITesting
{
    public class AccountTransactionTesting : IDisposable
    {
        private readonly AccountTransactionController _accountTransactionController;

        public AccountTransactionTesting()
        {
            _accountTransactionController = new AccountTransactionController();
        }

        [Fact(DisplayName = "Debe de realizar el déposito correctamente")]
        public void Deposit_Ok()
        {
            // Arrange
            var testAccount = new UserAccount()
            {
                Id = 1,
                AccountNumber = 1224,
                Name = "Juan",
                MiddleName = "José",
                LastName = "Mendoza",
                SecontLastName = "Zapata",
                Currency = UserAccount.ECurrency.NIO,
                Balance = 1000.00m,
            };

            AccountsDataBase.Current.UserAccounts.Add(testAccount);

            decimal depositAmount = 500.00m;

            var newBalance = testAccount.Balance + depositAmount;

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

        [Fact(DisplayName = "Debe de realizar el retiro correctamente")]
        public void Withdrawal_Ok()
        {
            // Arrange
            var testAccount = new UserAccount()
            {
                Id = 1,
                AccountNumber = 1224,
                Name = "Juan",
                MiddleName = "José",
                LastName = "Mendoza",
                SecontLastName = "Zapata",
                Currency = UserAccount.ECurrency.NIO,
                Balance = 1500.00m,
            };

            AccountsDataBase.Current.UserAccounts.Add(testAccount);

            decimal withdrawalAmount = 200.00m;

            var newBalance = testAccount.Balance - withdrawalAmount;

            var newTransaction = new AccountTransactionInsert()
            {
                AccountNumber = 1224,
                Amount = withdrawalAmount
            };

            // Act
            var result = (CreatedAtActionResult)_accountTransactionController.CreateWithdraw(newTransaction);
            var withdrawal = Assert.IsType<AccountTransaction>(result.Value);

            // Assert
            Assert.NotNull(withdrawal);
            Assert.Equal(withdrawal.Balance, newBalance);
        }

        [Fact(DisplayName = "La transacción debe de fallar por que no hay fondos suficientes")]
        public void Withdrawal_Failed()
        {
            // Arrange
            var testAccount = new UserAccount()
            {
                Id = 1,
                AccountNumber = 1224,
                Name = "Juan",
                MiddleName = "José",
                LastName = "Mendoza",
                SecontLastName = "Zapata",
                Currency = UserAccount.ECurrency.NIO,
                Balance = 1500.00m,
            };

            AccountsDataBase.Current.UserAccounts.Add(testAccount);

            decimal withdrawalAmount = 2000.00m;

            var newBalance = testAccount.Balance - withdrawalAmount;

            var newTransaction = new AccountTransactionInsert()
            {
                AccountNumber = 1224,
                Amount = withdrawalAmount
            };

            // Act
            var result = _accountTransactionController.CreateWithdraw(newTransaction);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        public void Dispose()
        {
            AccountsDataBase.Current.UserAccounts.Clear();
        }
    }
}
