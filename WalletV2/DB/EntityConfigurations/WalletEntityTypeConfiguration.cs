using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletV2.Models;

namespace WalletV2.DB.EntityConfigurations;

public class WalletEntityTypeConfiguration: IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("tbl_wallet");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Amount).HasColumnName("wallet_amount");
        builder.Property(e => e.UpdatedAt).HasColumnName("wallet_updated_at");
        builder.Property(e => e.AccountId)
            .HasColumnName("account_id");
        builder.HasOne(e => e.Account)
            .WithMany()
            .HasForeignKey(e => e.AccountId);
    }
}