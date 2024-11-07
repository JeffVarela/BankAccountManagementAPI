using BankAccountManagementAPI.Models;

namespace BankAccountManagementAPI.Services
{
    public class AccountsDataBase
    {
        // lista que almacena las cuentas de los usuarios
        public List<UserAccount> UserAccounts { get; set; }

        // Nos aseguramos que solo haya una instancia de AccountsDataBase
        public static AccountsDataBase Current { get; } = new AccountsDataBase();

        public AccountsDataBase()
        {
            UserAccounts = new List<UserAccount>() {};
        }
    }
}
