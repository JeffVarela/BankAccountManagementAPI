namespace BankAccountManagementAPI.Models
{
    public class AccountTransaction
    {
        public int TransactionId { get; set; }

        public string TransactionType { get; set; } = string.Empty;

        public decimal Amount { get; set; } = 0.0m;

        public decimal Balance { get; set; } = 0.0m;
    }
}
