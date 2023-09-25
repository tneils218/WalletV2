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
    GroupId = kafkaConfig.GroupId,
    BootstrapServers = kafkaConfig.BootstrapServers,
};
var producerConfig = new ProducerConfig
{
    BootstrapServers = kafkaConfig.BootstrapServers,
};
var services = builder.Services;
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MySql")));
services.AddSingleton<IDbContextFactory<AppDbContext>, AppDbContextFactory>();
services.AddHostedService<WalletQueueHandler>();
services.AddHostedService<AccountQueueHandler>();
services.AddSingleton<IAccountQueueService>(sp => new InMemoryAccountQueueService(128));
services.AddSingleton<IWalletQueueService>(sp => new InMemoryWalletQueueService(128));
services.AddSingleton<IWalletService, WalletService>();
services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton(sp =>
{
    var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
    return new KafkaConsumer<Ignore, string>(consumer);
});
builder.Services.AddSingleton(sp =>
{
    var producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    return new KafkaProducer<Null, string>(producer);
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:7016")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseFileServer();

app.MapControllers();

app.Run();