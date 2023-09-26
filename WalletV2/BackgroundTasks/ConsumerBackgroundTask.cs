using Confluent.Kafka;
using System.Text.Json;
using WalletV2.Services;
using WalletV2.Services.DTOs;

namespace WalletV2.BackgroundTasks
{
    public class ConsumerBackgroundTask : BackgroundService
    {
        private readonly KafkaConsumer<Ignore, string> _kafkaConsumer;
        private readonly ILogger<ConsumerBackgroundTask> _logger;
        private readonly IWalletService _walletService;

        public ConsumerBackgroundTask(KafkaConsumer<Ignore, string> kafkaConsumer, ILogger<ConsumerBackgroundTask> logger, IWalletService walletService)

        {
            _kafkaConsumer = kafkaConsumer;
            _logger = logger;
            _walletService = walletService;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var task = new TaskFactory().StartNew(() => _kafkaConsumer.Consume(ConsumerCallBack, "wallet-input", cancellationTokenSource.Token),
             cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            return task;
        }

        private async void ConsumerCallBack(ConsumeResult<Ignore, string> consumeResult)
        {
            var data = JsonSerializer.Deserialize<WalletQueueDto>(consumeResult.Message.Value);
            if (data != null)
            {
                await _walletService.TransferWallet(data.SourceId, data.WalletId, data.Amount, data.DestinationId, data.DestinationWalletId, data.ActionId);
            }
        }
    }
}