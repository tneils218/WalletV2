using System.ComponentModel;

namespace WalletV2.Controllers.Request
{
    public class WalletAddMoneyRequest
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
        
        [DefaultValue(1)]
        public int ActionTypeId { get; set; } = 1;
    }
}