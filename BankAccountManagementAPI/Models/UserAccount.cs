namespace BankAccountManagementAPI.Models
{
    public class UserAccount
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string MiddleName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string SecontLastName { get; set; } = string.Empty;

        public int AccountNumber { get; set; } = 0;

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public ECurrency Currency { get; set; }

        public decimal Balance { get; set; } = 0.0m;

        public enum ECurrency 
        {
            USD,
            NIO
        }

        public List<AccountTransaction> Transactions { get; set; } = new List<AccountTransaction>();
    }
}
