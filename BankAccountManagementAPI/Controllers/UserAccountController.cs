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
        // GetUserAccounts retorna una lista de todas las cuentas de los usuarios
        [HttpGet]
        public ActionResult<IEnumerable<UserAccount>> GetUserAccounts()
        {
            var accounts = AccountsDataBase.Current.UserAccounts;
            return Ok(accounts);
        }

        // GetUserAccount retorna la información de la cuenta del usuario con forme a su id de cuenta
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
            // Validamos que el monto inicial sea mayor a 1
            if (userAccountInsert.InicialAmount < 1)
                return BadRequest(Responses.AccountTransaction.InvalidAmount);

            // Generamos el id de la cuenta
            var accounts = AccountsDataBase.Current.UserAccounts;
            var accountId = accounts.Count > 0 ? (accounts.Max(a => a.Id) + 1) : 1;

            // Generamos el número de cuenta único
            int accountNumberCounter = 1000;
            var accountNumber = accounts.Count > 0 ? (accounts.Max(a => a.AccountNumber) + 1) : (accountNumberCounter + 1);

            // Creamos la nueva cuenta del usuario
            var newAccount = new UserAccount()
            {
                Id = accountId,
                Name = userAccountInsert.Name,
                MiddleName = userAccountInsert.MiddleName,
                LastName = userAccountInsert.LastName,
                SecontLastName = userAccountInsert.SecontLastName,
                AccountNumber = accountNumber,
                Currency = userAccountInsert.Currency,
                Balance = userAccountInsert.InicialAmount,
                Transactions =
                {
                    // creamos el primer movimiento del balance
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
