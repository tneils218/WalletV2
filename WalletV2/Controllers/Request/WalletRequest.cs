using System.ComponentModel.DataAnnotations;

namespace WalletV2.Controllers.Request
{
    public class WalletRequest

    {
        [Required]
        public int AccountId { get; set; }
    }
}
