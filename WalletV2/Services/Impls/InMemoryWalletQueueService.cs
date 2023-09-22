using System.Threading.Channels;
using WalletV2.Services.DTOs;

namespace WalletV2.Services.Impls;

public class InMemoryWalletQueueService : IWalletQueueService
{
    private readonly Channel<WalletQueueDto> _queue;

    public InMemoryWalletQueueService(int size)
    {
        var options = new BoundedChannelOptions(size)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        _queue = Channel.CreateBounded<WalletQueueDto>(options);
    }

    public async Task<WalletQueueDto> Dequeue()
    {
        var data = await _queue.Reader.ReadAsync();
        return data;
    }

    public async Task Queue(WalletQueueDto data)
    {
        await _queue.Writer.WriteAsync(data);
    }
}