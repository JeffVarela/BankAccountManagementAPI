using static BankAccountManagementAPI.Models.UserAccount;

namespace BankAccountManagementAPI.Models
{
    public class UserAccountInsert
    {
        public string Name { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string SecontLastName { get; set; } = string.Empty;

        public int AccountNumber { get; set; } = 0;

        public decimal InicialAmount { get; set; }

        public ECurrency Currency { get; set; }
    }
}
