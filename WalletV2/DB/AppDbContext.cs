using Microsoft.EntityFrameworkCore;
using WalletV2.DB.EntityConfigurations;
using WalletV2.Models;

namespace WalletV2.DB;

public class AppDbContext: DbContext
{
    public AppDbContext()
    {
    }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Account> AccountDb { get; set; } = null!;
    
    public DbSet<AccountType> AccountTypeDb { get; set; } = null!;

    public DbSet<ActionType> ActionTypeDb { get; set; } = null!;
    
    public DbSet<ActionFee> ActionDb { get; set; } = null!;
    
    public DbSet<Wallet> WalletDb { get; set; } = null!;

    public DbSet<WalletHistory> WalletHistoryDb { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AccountTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WalletEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WalletHistoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ActionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ActionTypeEntityTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}