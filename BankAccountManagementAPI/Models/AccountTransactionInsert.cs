using System.ComponentModel.DataAnnotations;

namespace BankAccountManagementAPI.Models
{
    public class AccountTransactionInsert
    {
        [Required]
        public decimal Amount { get; set; } = 0.0m;

        [Required]
        public int AccountNumber { get; set; }
    }
}
