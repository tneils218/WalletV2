using System.Text.Json.Serialization;

namespace WalletV2.Services.DTOs
{
    public class WalletQueueDto
    {
        public int ActionId { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
        public int DestinationId { get; set; }
        public int DestinationWalletId { get; set; }
        public int SourceId { get; set; }

        [JsonConstructor]
        public WalletQueueDto(int actionId, int walletId, decimal amount, int sourceId, int destinationId, int destinationWalletId)
        {
            ActionId = actionId;
            WalletId = walletId;
            Amount = amount;
            DestinationId = destinationId;
            SourceId = sourceId;
            DestinationWalletId = destinationWalletId;
        }

        public WalletQueueDto(int walletId, decimal amount, int actionId)
        {
            WalletId = walletId;
            Amount = amount;
            ActionId = actionId;
        }

        public static WalletQueueDto Create(int actionId, int walletId, decimal amount, int sourceId, int destinationId, int destinationWalletId)
        {
            return new WalletQueueDto(actionId, walletId, amount, sourceId, destinationId, destinationWalletId);
        }

        public static WalletQueueDto CreateForAdd(int walletId, decimal amount, int actionTypeId)
        {
            return new WalletQueueDto(walletId, amount, actionTypeId);
        }
    }
}
