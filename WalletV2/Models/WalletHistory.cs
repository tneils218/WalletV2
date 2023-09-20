namespace WalletV2.Models;

   public class WalletHistory
    {
        public int Id { get; set; }
        public Wallet? Wallet { get; set; }
        public int WalletId { get; set; }

        public decimal Amount { get; set; }

        public AccountType? AccountType { get; set; } = null!;

        public ActionType? ActionType { get; set; } = null!;

        public int AccountTypeId { get; set; }

        public int ActionTypeId { get; set; }

        public int? SourceWalletId { get; set; }
        public int? DestinationWalletId { get; set; }

        public decimal Fee { get; set; } = 0;

        public DateTime CreatedAt { get; set; }

        private WalletHistory()
        {
            CreatedAt = DateTime.Now;
        }

        public static WalletHistory CreateForSender(int walletId, int receiverWalletId, decimal fee, int accountTypeId, int actionTypeId, decimal amount)
        {

            return new WalletHistory
            {
                DestinationWalletId = receiverWalletId,
                Fee = fee,
                AccountTypeId = accountTypeId,
                ActionTypeId = actionTypeId,
                Amount = amount,
                WalletId = walletId,
            };
        }
        public static WalletHistory CreateForReceiver(int walletId, int senderWalletId, decimal fee, int accountTypeId, int actionTypeId, decimal amount)
        {
            return new WalletHistory
            {
                SourceWalletId = senderWalletId,
                Fee = fee,
                AccountTypeId = accountTypeId,
                ActionTypeId = actionTypeId,
                Amount = amount,
                WalletId = walletId,

            };
        }
        public static WalletHistory CreateForAddMoney(int walletId, int senderWalletId, decimal fee, int accountTypeId, int actionTypeId, decimal amount)
        {
            return new WalletHistory
            {
                SourceWalletId = senderWalletId,
                Fee = fee,
                AccountTypeId = accountTypeId,
                ActionTypeId = actionTypeId,
                Amount = amount,
                WalletId = walletId,

            };
        }
    }