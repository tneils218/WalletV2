using Confluent.Kafka;
using System.Text.Json;
using WalletV2.Models;

namespace WalletV2.BackgroundTasks
{
    public class ConsumerBackgroundTaskOutput : BackgroundService
    {
        private readonly KafkaConsumer<Ignore, string> _kafkaConsumer;
        private readonly ILogger<ConsumerBackgroundTask> _logger;

        public ConsumerBackgroundTaskOutput(KafkaConsumer<Ignore, string> kafkaConsumer, ILogger<ConsumerBackgroundTask> logger)
        {
            _kafkaConsumer = kafkaConsumer;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var task = new TaskFactory().StartNew(() => _kafkaConsumer.Consume(ConsumerCallBack, "wallet-output", cancellationTokenSource.Token),
             cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            return task;
        }

        private async void ConsumerCallBack(ConsumeResult<Ignore, string> consumeResult)
        {
            var data = JsonSerializer.Deserialize<WalletHistory>(consumeResult.Message.Value);

        }
    }
}
