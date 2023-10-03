using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WalletV2.DB;
using WalletV2.Models;
using WalletV2.Services.DTOs;

namespace WalletV2.Services.Impls;

public class WalletService : IWalletService
{
    private readonly IDbContextFactory<AppDbContext> _dbContextContextFactory;
    private readonly ILogger<WalletService> _logger;
    private readonly KafkaProducer<Null, string> _kafkaProduce;

    public WalletService(IDbContextFactory<AppDbContext> dbContextFactory, ILogger<WalletService> logger, KafkaProducer<Null, string> kafkaProduce)
    {
        _dbContextContextFactory = dbContextFactory;
        _logger = logger;
        _kafkaProduce = kafkaProduce;
    }

    public async Task<Wallet> CreateWallet(WalletDto walletDto)
    {
        using (var dbContext = _dbContextContextFactory.CreateDbContext())
        {
            var wallet = new Wallet(walletDto.AccountId);

            dbContext.WalletDb.Add(wallet);
            await dbContext.SaveChangesAsync();
            return wallet;
        }
    }

    public async Task<List<Wallet>> GetAllWallet(string id)
    {
        using (var dbContext = _dbContextContextFactory.CreateDbContext())
        {
            IQueryable<Wallet> query = dbContext.WalletDb.Include(o => o.Account); ;
            if (!string.IsNullOrEmpty(id))
            {
                query = query.Where(o => o.AccountId == int.Parse(id));
            }

            var wallets = await query.ToListAsync();
            return wallets;
        }
    }

    public async Task<string> TransferWallet(int sourceId, int walletId, decimal amount, int destinationId, int destinationWalletId, int actionTypeId)
    {
        using (var dbContext = _dbContextContextFactory.CreateDbContext())
        {
            if (walletId == destinationWalletId && sourceId == destinationId)
            {
                throw new InvalidOperationException("Không thể chuyển cùng ví");
            }

            using var transaction = await dbContext.Database.BeginTransactionAsync();

            var sender = await dbContext.WalletDb.Include(o => o.Account)
                .FirstOrDefaultAsync(o => o.AccountId == sourceId && o.Id == walletId);

            var receiver = await dbContext.WalletDb.Include(o => o.Account)
                .FirstOrDefaultAsync(o => o.AccountId == destinationId && o.Id == destinationWalletId);

            if (sender == null || receiver == null)
            {
                throw new ArgumentException("Invalid sender or receiver wallet ID.");
            }

            var transferFee = await dbContext.ActionDb
                .FirstOrDefaultAsync(o => o.AccountTypeId == sender!.Account!.AccountTypeId && o.ActionTypeId == actionTypeId);

            if (transferFee == null)
            {
                throw new InvalidOperationException("Transfer fee not available for the given account type.");
            }

            if (sender.Amount < amount)
            {
                throw new InvalidOperationException("Insufficient balance in the sender's wallet.");
            }

            try
            {
                sender.Amount -= amount + transferFee.Fee;
                receiver.Amount += amount;

                var walletTransferHistory = WalletHistory.CreateForSender(sender.Id, destinationWalletId, transferFee.Fee, sender!.Account!.AccountTypeId, actionTypeId, amount);
                var data = JsonSerializer.Serialize(walletTransferHistory);
                var message = new Message<Null, string> { Value = data };
                _kafkaProduce.Produce(message, "wallet-output");
                dbContext.WalletHistoryDb.Add(walletTransferHistory);
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return "Chuyển khoản thành công";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }

    public async Task<Wallet> UpdateWallet(WalletQueueDto walletQueueDto)
    {
        using (var dbContext = _dbContextContextFactory.CreateDbContext())
        {
            try
            {
                var wallet = await dbContext.WalletDb.Include(o => o.Account).FirstOrDefaultAsync(o => o.Id == walletQueueDto.WalletId);

                var transferFee = await dbContext.ActionDb
                    .FirstOrDefaultAsync(o => o.AccountTypeId == wallet!.Account!.AccountTypeId && o.ActionTypeId == walletQueueDto.ActionId);

                if (wallet != null)
                {
                    switch (walletQueueDto.ActionId)
                    {
                        case 1:
                            wallet.Amount += walletQueueDto.Amount;
                            break;

                        default:
                            if (wallet.Amount > 0 && wallet.Amount >= walletQueueDto.Amount)
                            {
                                wallet.Amount -= walletQueueDto.Amount;
                            }
                            break;
                    }


                    var walletTransferHistory = WalletHistory.CreateForAddMoney(walletQueueDto.WalletId, wallet.Id, transferFee.Fee, wallet!.Account!.AccountTypeId, walletQueueDto.ActionId, walletQueueDto.Amount);
                    var data = JsonSerializer.Serialize(walletTransferHistory);
                    var message = new Message<Null, string> { Value = data };
                    _kafkaProduce.Produce(message, "wallet-output");
                    await dbContext.SaveChangesAsync();

                    return wallet;
                }
                else
                {
                    throw new Exception("Wallet not found."); // Custom exception for not found case
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating wallet.", ex); // Custom exception for other errors
            }
        }
    }
}
