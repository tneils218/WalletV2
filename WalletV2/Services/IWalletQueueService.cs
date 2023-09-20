using WalletV2.Services.DTOs;

namespace WalletV2.Services;

public interface IWalletQueueService
{
    Task Queue(WalletQueueDto data);
    Task<WalletQueueDto> Dequeue();
}

