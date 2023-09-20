using Microsoft.EntityFrameworkCore;
using WalletV2.DB;
using WalletV2.Models;
using WalletV2.Services.DTOs;

namespace WalletV2.Services.Impls;

public class AccountService: IAccountService
{
    private readonly IDbContextFactory<AppDbContext> dbContextContextFactory;
    private readonly ILogger<AccountService> _logger;
    public AccountService(IDbContextFactory<AppDbContext> dbContextFactory, ILogger<AccountService> logger)
    {
        dbContextContextFactory = dbContextFactory;
        _logger = logger;

    }
    public async Task<List<Account>> GetAccounts()
    {
        using (var dbContext = dbContextContextFactory.CreateDbContext())
        {
            var accounts = await dbContext.AccountDb.Include(a => a.AccountType).ToListAsync();
            return accounts;
        }
    }

    public async Task<Account> CreateAccount(AccountDto accountDto)
    {
        using (var dbContext = dbContextContextFactory.CreateDbContext())
        {
            var account = new Account(accountDto.UserName, accountDto.FullName, accountDto.Email, accountDto.Dob, accountDto.AccountTypeId);

            dbContext.AccountDb.Add(account);
            await dbContext.SaveChangesAsync();
            return account;
        }
    }
}