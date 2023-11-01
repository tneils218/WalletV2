using WalletV2.Models;
using WalletV2.Services.DTOs;

namespace WalletV2.Services;

public interface IWalletService
{
    Task<Wallet> CreateWallet(WalletDto walletDto);
    Task<string> TransferWallet(int sourceId, int walletId, decimal amount, int destinationId, int destinationWalletId, int actionTypeId);

    Task<List<Wallet>> GetAllWallet(string id);

    Task<Wallet> UpdateWallet(WalletQueueDto walletQueueDto);

}
