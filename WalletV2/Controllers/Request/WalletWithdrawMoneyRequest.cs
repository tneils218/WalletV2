using System.ComponentModel;

namespace WalletV2.Controllers.Request;

public class WalletWithdrawMoneyRequest
{
    public int Id { get; set; }
    public int WalletId { get; set; }
    public decimal Amount { get; set; }
    [DefaultValue(4)]
    public int ActionTypeId { get; set; } = 4;
}