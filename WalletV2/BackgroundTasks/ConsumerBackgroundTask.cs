using Confluent.Kafka;

namespace WalletV2.BackgroundTasks
{
    public class ConsumerBackgroundTask : BackgroundService
    {
        private readonly KafkaConsumer<Ignore, string> _kafkaConsumer;
        private readonly ILogger<ConsumerBackgroundTask> _logger;

        public ConsumerBackgroundTask(KafkaConsumer<Ignore, string> kafkaConsumer, ILogger<ConsumerBackgroundTask> logger)
        {
            _kafkaConsumer = kafkaConsumer;
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var task = new TaskFactory().StartNew(() => _kafkaConsumer.Consume(ConsumerCallBack, "wallet-input", 1, -1, cancellationTokenSource.Token),
             cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            return task;
        }

        private async void ConsumerCallBack(ConsumeResult<Ignore, string> consumeResult)
        {
        }
    }
}