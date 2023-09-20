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
        while (!stoppingToken.IsCancellationRequested)
        {
            var data = await _queueService.Dequeue();
            switch (data.ActionId)
            {
                case 1:
                case 4:
                    
                    break;

                case 3:
                    break;
                default:
                    break;
                    
            }

        }

    }
}