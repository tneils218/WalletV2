using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WalletV2.Controllers.Request
{
    public class WalletTransferRequest
    {
        public int ReceiverId { get; set; }
        public int ReceiverWalletId { get; set; }

        [Required][DefaultValue(2)] public int ActionTypeId { get; set; }

        [Required][Range(1, double.MaxValue)] public decimal Amount { get; set; }
    }
}