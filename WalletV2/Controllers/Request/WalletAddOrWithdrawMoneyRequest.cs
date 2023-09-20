namespace WalletV2.Controllers.Request
{
    public class WalletAddOrWithdrawMoneyRequest
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
        public int ActionTypeId { get; set; }
    }
}