using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletV2.Models;

namespace WalletV2.DB.EntityConfigurations;

public class AccountTypeEntityTypeConfiguration: IEntityTypeConfiguration<AccountType>
{
    public void Configure(EntityTypeBuilder<AccountType> builder)
    {
        builder.ToTable("tbl_account_type");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).HasColumnName("account_type_name").HasMaxLength(50);
        builder.HasData(CreateAccountType());
    }
    private AccountType[] CreateAccountType()
    {
        return new AccountType[]
        {
            new AccountType(1, "normal"),
            new AccountType(2, "premium"),
            new AccountType(3, "vip"),
        };
    }
}