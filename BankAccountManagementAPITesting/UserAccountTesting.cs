using BankAccountManagementAPI.Controllers;
using BankAccountManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountManagementAPITesting
{
    public class UserAccountTesting
    {
        private readonly UserAccountController _userAccountController;

        public UserAccountTesting()
        {
            _userAccountController = new UserAccountController();
        }

        [Fact(DisplayName = "La creación de la cuenta con el saldo inicial")]
        public void InicialBalance_Exists()
        {
            decimal InicialAmount = 1000.00m;

            var newAccount = new UserAccountInsert()
            {
                AccountNumber = 1224,
                Name = "Jefferson",
                MiddleName = "Javier",
                LastName = "Varela",
                SecontLastName = "Quiroz",
                Currency = UserAccount.ECurrency.NIO,
                InicialAmount = InicialAmount
            };

            var result = (CreatedAtActionResult)_userAccountController.CreateAccount(newAccount);
            var account = Assert.IsType<UserAccount>(result.Value);

            Assert.Equal(account.Balance, InicialAmount);
            Assert.Equal(account.Transactions[0].Balance, InicialAmount);
        }
    }
}