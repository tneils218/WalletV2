using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletV2.Models;

namespace WalletV2.DB.EntityConfigurations;

public class WalletHistoryEntityTypeConfiguration : IEntityTypeConfiguration<WalletHistory>
{
    public void Configure(EntityTypeBuilder<WalletHistory> builder)
    {
        builder.ToTable("tbl_wallet_history");
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.Wallet)
            .WithMany()
            .HasForeignKey(e => e.WalletId);

        builder.Property(e => e.AccountTypeId)
            .HasColumnName("wallet_account_type");
        builder.HasOne(e => e.AccountType)
            .WithMany()
            .HasForeignKey(e => e.AccountTypeId);
        builder.Property(e => e.ActionTypeId)
            .HasColumnName("wallet_action_type");
        builder.HasOne(e => e.ActionType)
            .WithMany()
            .HasForeignKey(e => e.ActionTypeId);

        builder.Property(e => e.Amount).HasColumnName("wallet_amount");
        builder.Property(e => e.SourceWalletId).HasColumnName("source_wallet_id");
        builder.Property(e => e.DestinationWalletId).HasColumnName("destination_wallet_id");
        builder.Property(e => e.CreatedAt).HasColumnName("wallet_created_at");
        builder.Property(e => e.Fee).HasColumnName("wallet_fee");
    }
}
