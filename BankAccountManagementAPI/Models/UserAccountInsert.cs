using System.ComponentModel.DataAnnotations;
using static BankAccountManagementAPI.Models.UserAccount;

namespace BankAccountManagementAPI.Models
{
    public class UserAccountInsert
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        public string MiddleName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string SecontLastName { get; set; } = string.Empty;

        [Required]
        public decimal InicialAmount { get; set; }

        [Required]
        public ECurrency Currency { get; set; }
    }
}
