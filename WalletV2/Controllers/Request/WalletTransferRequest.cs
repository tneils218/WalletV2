using System.ComponentModel.DataAnnotations;

namespace WalletV2.Controllers.Request
{
    public class WalletTransferRequest
    {
        public int ReceiverId { get; set; }
        public int ReceiverWalletId { get; set; }

        [Required] public int ActionTypeId { get; set; }

        [Required] [Range(1, double.MaxValue)] public decimal Amount { get; set; }
    }
}