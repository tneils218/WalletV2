using WalletV2.Services;

namespace WalletV2.BackgroundTasks;

public class AccountQueueHandler : BackgroundService
{
    private readonly IAccountQueueService _queueService;
    private readonly IAccountService _accountService;
    private readonly ILogger<AccountQueueHandler> _logger;

    public AccountQueueHandler(IAccountQueueService queueService, IAccountService accountService, ILogger<AccountQueueHandler> logger)
    {
        _queueService = queueService;
        _accountService = accountService;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var data = await _queueService.Dequeue();
        }
    }
}