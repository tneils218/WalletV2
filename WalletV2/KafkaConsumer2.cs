using Confluent.Kafka;

namespace WalletV2;

public class KafkaConsumer2<TKey, TValue> : IDisposable
{
    private readonly IConsumer<TKey, TValue> _consumer2;

    public KafkaConsumer2(IConsumer<TKey, TValue> consumer2)
    {
        _consumer2 = consumer2;
    }

    public void Dispose()
    {
        _consumer2.Dispose();
    }

    public void Consume(Action<ConsumeResult<TKey, TValue>> callback, string topic, CancellationToken cancellationToken = default)
    {
        SetSubscribeOrAssign(topic);
        while (!cancellationToken.IsCancellationRequested)
        {
            var result = _consumer2.Consume(TimeSpan.FromSeconds(1));
            if (result != null)
            {
                callback(result);
            }
        }
    }

    private void SetSubscribeOrAssign(string topic)
    {
        _consumer2.Subscribe(topic);
    }
}
