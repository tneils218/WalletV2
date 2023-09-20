using System.ComponentModel.DataAnnotations;

namespace WalletV2.Controllers.Request
{
    public class AccountRequest
    {
        [Required]
        public int AccountTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = null!;
        [Required]
        [MaxLength(200)]
        public string FullName { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        [EmailAddress]
        public string Email { get; set; } = null!;


        [Required]
        public DateTime Dob { get; set; }
    }
}
