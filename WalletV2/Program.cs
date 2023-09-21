using Microsoft.EntityFrameworkCore;
using WalletV2.BackgroundTasks;
using WalletV2.DB;
using WalletV2.Services;
using WalletV2.Services.Impls;

var builder = WebApplication.CreateBuilder(args);

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


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseFileServer();

app.MapControllers();

app.Run();