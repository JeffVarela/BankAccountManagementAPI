namespace BankAccountManagementAPI.Models
{
    public class AccountTransactionInsert
    {
        public decimal Amount { get; set; } = 0.0m;

        public int AccountNumber { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
