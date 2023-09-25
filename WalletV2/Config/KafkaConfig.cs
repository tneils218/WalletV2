namespace WalletV2.Models
{
    public class KafkaConfig
    {
        public string BootstrapServers { get; set; } = null!;

        public string GroupId { get; set; } = null!;

        public string InputTopic { get; set; } = null!;
        public string OutputTopic { get; set; } = null!;
    }
}