using Microsoft.EntityFrameworkCore;

namespace WalletV2.DB;

public class AppDbContextFactory : IDbContextFactory<AppDbContext>
{
    private readonly string _dbConnStr;

    public AppDbContextFactory(IConfiguration configuration)
    {
        _dbConnStr = configuration.GetConnectionString("MySql");
    }
    public AppDbContext CreateDbContext()
    {
        var optionsBuiler = new DbContextOptionsBuilder<AppDbContext>().UseMySQL(_dbConnStr);
        return new AppDbContext(optionsBuiler.Options);
    }
}