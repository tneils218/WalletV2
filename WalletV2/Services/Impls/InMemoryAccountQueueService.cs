using System.Threading.Channels;
using WalletV2.Services.DTOs;

namespace WalletV2.Services.Impls;

public class InMemoryAccountQueueService: IAccountQueueService
{
    
    private readonly Channel<AccountDto> _queue;

    public InMemoryAccountQueueService(int size)
    {
        var options = new BoundedChannelOptions(size)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        _queue = Channel.CreateBounded<AccountDto>(options);
    }

    public async Task<AccountDto> Dequeue()
    {
        var data = await _queue.Reader.ReadAsync();
        return data;
    }

    public async Task Queue(AccountDto data)
    {
        await _queue.Writer.WriteAsync(data);
    }
}