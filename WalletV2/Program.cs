using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using WalletV2;
using WalletV2.BackgroundTasks;
using WalletV2.DB;
using WalletV2.Models;
using WalletV2.Services;
using WalletV2.Services.Impls;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var kafkaConfig = configuration.GetSection("KafkaConfig").Get<KafkaConfig>();

var consumerConfig = new ConsumerConfig
{
    GroupId = kafkaConfig.ConsumerGroupId,
    BootstrapServers = kafkaConfig.BootstrapServers,
    SessionTimeoutMs = 6000,
    QueuedMinMessages = 1000000
};
var consumerConfig2 = new ConsumerConfig
{
    GroupId = kafkaConfig.ConsumerGroupId2,
    BootstrapServers = kafkaConfig.BootstrapServers,
    SessionTimeoutMs = 6000,
    QueuedMinMessages = 1000000
};
var producerConfig = new ProducerConfig
{
    BootstrapServers = kafkaConfig.BootstrapServers,
};
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});
var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddSignalR();
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MySql")));
services.AddSingleton<IAccountService, AccountService>();
services.AddSingleton<IAccountQueueService>(sp => new InMemoryAccountQueueService(128));
services.AddSingleton<IWalletQueueService>(sp => new InMemoryWalletQueueService(128));
services.AddSingleton<IWalletService, WalletService>();
services.AddSingleton<IDbContextFactory<AppDbContext>, AppDbContextFactory>();
builder.Services.AddHostedService<ConsumerBackgroundTask>();
builder.Services.AddHostedService<ConsumerBackgroundTaskOutput>();

builder.Services.AddSingleton(sp =>
{
    var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
    return new KafkaConsumer<Ignore, string>(consumer);
});
builder.Services.AddSingleton(sp =>
{
    var consumer2 = new ConsumerBuilder<Ignore, string>(consumerConfig2).Build();
    return new KafkaConsumer2<Ignore, string>(consumer2);
});

builder.Services.AddSingleton(sp =>
{
    var producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    return new KafkaProducer<Null, string>(producer);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseFileServer();

app.UseCors();

app.MapControllers();
app.MapHub<SignalRHub>("/hub/Wallet");
app.Run();
