using WalletV2.Services;

namespace WalletV2.BackgroundTasks;

public class WalletQueueHandler : BackgroundService
{
    private readonly IWalletQueueService _queueService;
    private readonly IWalletService _walletService;
    private readonly ILogger<WalletQueueHandler> _logger;

    public WalletQueueHandler(IWalletQueueService queueService, IWalletService walletService, ILogger<WalletQueueHandler> logger)
    {
        _queueService = queueService;
        _walletService = walletService;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var data = await _queueService.Dequeue();
                switch (data.ActionId)
                {
                    case 1:
                    case 4:
                        //await _walletService.UpdateWallet(data.WalletId, data.Amount, data.ActionId);
                        break;

                    case 3:
                        // Handle case 3 logic
                        break;

                    default:
                        await _walletService.TransferWallet(data.SourceId, data.WalletId, data.Amount, data.DestinationId, data.DestinationWalletId, data.ActionId);
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
