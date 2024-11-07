using BankAccountManagementAPI.Helpers;
using BankAccountManagementAPI.Models;
using BankAccountManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<UserAccount>> GetUserAccounts()
        {
            var accounts = AccountsDataBase.Current.UserAccounts;
            return Ok(accounts);
        }

        [HttpGet("{accountId}")]
        public ActionResult<UserAccount> GetUserAccount(int accountId) 
        {
            var account = AccountsDataBase.Current.UserAccounts.FirstOrDefault(a => a.Id == accountId);

            if (account == null)
                return NotFound(Responses.UserAccount.NotFound);

            return Ok(account);
        }

        // GetUserBalance consultará el saldo actual del usuario conforme a su número de cuenta.
        [HttpGet("balance/{accountNumber}")]
        public ActionResult<UserAccount> GetUserBalance(int accountNumber)
        {
            var account = AccountsDataBase.Current.UserAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (account == null)
                return NotFound(Responses.UserAccount.NotFound);

            return Ok ( new 
                {
                        Name = $"{account.Name} {(account.LastName ?? account.SecontLastName)}",
                        Balance = account.Balance,
                });
        }

        // CreateAccount crea una nueva cuenta de usuario con un monto inicial.
        [HttpPost]
        public ActionResult CreateAccount(UserAccountInsert userAccountInsert)
        {
            // Generamos el id de la cuenta
            var accounts = AccountsDataBase.Current.UserAccounts;
            var accountId = accounts.Count > 0 ? (accounts.Max(a => a.Id) + 1) : 1;

            var newAccount = new UserAccount()
            {
                Id = accountId,
                Name = userAccountInsert.Name,
                MiddleName = userAccountInsert.MiddleName,
                LastName = userAccountInsert.LastName,
                SecontLastName = userAccountInsert.SecontLastName,
                AccountNumber = userAccountInsert.AccountNumber,
                Currency = userAccountInsert.Currency,
                Balance = userAccountInsert.InicialAmount,
                Transactions =
                {
                    new AccountTransaction()
                    {
                        // El id del primer movimiento siempre será 1 y el típo será depósito
                        TransactionId = 1,
                        Amount = userAccountInsert.InicialAmount,
                        Balance = userAccountInsert.InicialAmount,
                        TransactionType = "Depósito"
                    }
                }
            };

            AccountsDataBase.Current.UserAccounts.Add(newAccount);

            return CreatedAtAction(nameof(GetUserAccount),
                new {accountId = newAccount.Id},
                newAccount);
        }
    }
}
