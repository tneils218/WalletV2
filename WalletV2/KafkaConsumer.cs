using Confluent.Kafka;

namespace WalletV2;

public class KafkaConsumer<TKey, TValue> : IDisposable
{
    private readonly IConsumer<TKey, TValue> _consumer;

    public KafkaConsumer(IConsumer<TKey, TValue> consumer)
    {
        _consumer = consumer;
    }

    public void Dispose()
    {
        _consumer.Dispose();
    }

    public void Consume(Action<ConsumeResult<TKey, TValue>> callback, string topic, CancellationToken cancellationToken = default)
    {
        SetSubscribeOrAssign(topic);
        while (!cancellationToken.IsCancellationRequested)
        {
            var result = _consumer.Consume(TimeSpan.FromSeconds(1));
            if (result != null)
            {
                callback(result);
            }
        }
    }

    private void SetSubscribeOrAssign(string topic)
    {
        _consumer.Subscribe(topic);
    }
}