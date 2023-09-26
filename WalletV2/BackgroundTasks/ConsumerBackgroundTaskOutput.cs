using Confluent.Kafka;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using WalletV2.Models;

namespace WalletV2.BackgroundTasks
{
    public class ConsumerBackgroundTaskOutput : BackgroundService
    {
        private readonly KafkaConsumer2<Ignore, string> _kafkaConsumer2;
        private readonly ILogger<ConsumerBackgroundTask> _logger;
        private readonly IHubContext<SignalRHub> _hubContext;

        public ConsumerBackgroundTaskOutput(KafkaConsumer2<Ignore, string> kafkaConsumer2, ILogger<ConsumerBackgroundTask> logger, IHubContext<SignalRHub> hubContext)
        {
            _kafkaConsumer2 = kafkaConsumer2;
            _logger = logger;
            _hubContext = hubContext;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var task = new TaskFactory().StartNew(() => _kafkaConsumer2.Consume(ConsumerCallBack, "wallet-output", cancellationTokenSource.Token),
             cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            return task;
        }

        private async void ConsumerCallBack(ConsumeResult<Ignore, string> consumeResult)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            await _hubContext.Clients.All.SendAsync("ReceiveData", JsonSerializer.Deserialize<WalletHistory>(consumeResult.Message.Value), cancellationTokenSource.Token);
        }
    }
}