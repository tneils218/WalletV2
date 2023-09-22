using System.ComponentModel.DataAnnotations;

namespace WalletV2.Controllers.Request;

public class AccountStatusActive
{
    [Required]
    public int AccountId { get; set; }
}