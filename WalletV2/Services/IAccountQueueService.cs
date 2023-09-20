using WalletV2.Services.DTOs;

namespace WalletV2.Services;

public interface IAccountQueueService
{
    Task Queue(AccountDto data);
    Task<AccountDto> Dequeue();
}