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


    public async Task<Account> ActiveStatus(int id)
    {
        using (var dbContext = dbContextContextFactory.CreateDbContext())
        {
            var account = await dbContext.AccountDb.FindAsync(id);

            if (account != null)
            {
                // Check if the account is already active
                if (account.Status)
                {
                    throw new InvalidOperationException("Account is already active.");
                }

                // Activate the account
                account.Active();

                // Update the activated timestamp
                account.ActivatedAt = DateTime.Now;

                await dbContext.SaveChangesAsync();

                return account;
            }

            throw new InvalidOperationException("Account not found");
        }
    }

}