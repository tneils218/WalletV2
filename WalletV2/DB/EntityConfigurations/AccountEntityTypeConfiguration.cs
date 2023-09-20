using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletV2.Models;

namespace WalletV2.DB.EntityConfigurations;

public class AccountEntityTypeConfiguration: IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("tbl_account");

        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.UserName)
            .IsUnique();
        builder.Property(e => e.UserName).HasColumnName("user_name");
        builder.Property(e => e.Dob).HasColumnName("dob");

        builder.Property(e => e.Email).
            HasColumnName("email")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(e => e.Email)
            .IsUnique();

        builder.Property(e => e.FullName)
            .HasColumnName("full_name")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(e => e.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(e => e.AccountTypeId)
            .HasColumnName("account_type_id");

        builder.Property(e => e.ActivatedAt)
            .HasColumnName("activated_at");

        builder.Property(e => e.Status).HasColumnName("status");

        builder.HasOne(e => e.AccountType)
            .WithMany()
            .HasForeignKey(e => e.AccountTypeId);
    } 
}