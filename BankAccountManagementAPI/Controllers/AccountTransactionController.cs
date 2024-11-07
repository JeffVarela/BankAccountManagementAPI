using BankAccountManagementAPI.Helpers;
using BankAccountManagementAPI.Models;
using BankAccountManagementAPI.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace BankAccountManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountTransactionController : ControllerBase
    {
        // Showrecord muestra el saldo actual y el resumen de todos los movimientos
        [HttpGet("record/{accountNumber}")]
        public ActionResult<AccountTransaction> ShowRecord(int accountNumber)
        {
            var account = AccountsDataBase.Current.UserAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (account == null)
                return NotFound(Responses.UserAccount.NotFound);

            return Ok(account);
        }

        // GetTransaction muestra la información de una transacción.
        [HttpGet("{accountNumber}/{transactionId}")]
        public ActionResult<AccountTransaction> GetTransaction(int accountNumber, int transactionId)
        {
            var account = AccountsDataBase.Current.UserAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (account == null)
                return NotFound(Responses.UserAccount.NotFound);

            var transaction = account.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);

            if (transaction == null)
                return NotFound(Responses.AccountTransaction.NotFound);

            return Ok(transaction);
        }

        // CreateDeposit crea un nuevo depósito
        [HttpPost("deposit")]
        public ActionResult CreateDeposit(AccountTransactionInsert accountTransactionInsert)
        {
            var account = AccountsDataBase.Current.UserAccounts.FirstOrDefault(a => a.AccountNumber == accountTransactionInsert.AccountNumber);

            // validar que exista la cuenta
            if (account == null)
                return NotFound(Responses.UserAccount.NotFound);

            // Generamos el id de la transacción
            var transactionId = account.Transactions.Count > 0 ? (account.Transactions.Max(a => a.TransactionId)) : 1;

            // Calculamos el nuevo balance
            var newBalance = account.Balance + accountTransactionInsert.Amount;

            // Creamos el registro del movimiento y actualizamos el balance
            var newTransaction = new AccountTransaction()
            {
                TransactionId = transactionId,
                Amount = accountTransactionInsert.Amount,
                Balance = newBalance,
                TransactionType = "Depósito"
            };

            account.Balance = newBalance;
            account.Transactions.Add(newTransaction);

            return CreatedAtAction(nameof(GetTransaction),
                new {accountNumber = account.AccountNumber, transactionId = newTransaction.TransactionId}
                , newTransaction);
        }

        // CreateDeposit crea un nuevo retiro
        [HttpPost("withdraw")]
        public ActionResult CreateWithdraw(AccountTransactionInsert accountTransactionInsert)
        {
            var account = AccountsDataBase.Current.UserAccounts.FirstOrDefault(a => a.AccountNumber == accountTransactionInsert.AccountNumber);

            // validar que exista la cuenta
            if (account == null)
                return NotFound(Responses.UserAccount.NotFound);

            // Validar que el usuario tenga saldo suficiente
            if (account.Balance < accountTransactionInsert.Amount)
                return BadRequest(Responses.AccountTransaction.NoBalance);

            // Calculamos el nuevo balance
            var newBalance = account.Balance - accountTransactionInsert.Amount;

            // Generamos el id de la transacción
            var transactionId = account.Transactions.Count > 0 ? (account.Transactions.Max(a => a.TransactionId)) : 1;

            // Creamos el registro del movimiento y actualizamos el balance
            var newTransaction = new AccountTransaction()
            {
                TransactionId = transactionId,
                Amount = accountTransactionInsert.Amount,
                Balance = newBalance,
                TransactionType = "Retiro"
            };

            account.Balance = newBalance;
            account.Transactions.Add(newTransaction);

            return CreatedAtAction(nameof(GetTransaction),
                new { accountNumber = account.AccountNumber, transactionId = newTransaction.TransactionId }
                , newTransaction);
        }
    }
}
