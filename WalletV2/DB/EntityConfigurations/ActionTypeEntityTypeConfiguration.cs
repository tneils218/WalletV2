using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WalletV2.Models;

namespace WalletV2.DB.EntityConfigurations;

public class ActionTypeEntityTypeConfiguration: IEntityTypeConfiguration<ActionFee>
{
    public void Configure(EntityTypeBuilder<ActionFee> builder)
    {
        builder.ToTable("tbl_action_fee");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.AccountTypeId)
            .HasColumnName("action_fee_account_type");
        builder.HasOne(e => e.AccountType)
            .WithMany()
            .HasForeignKey(e => e.AccountTypeId);
        builder.Property(e => e.ActionTypeId)
            .HasColumnName("action_fee_action_type");
        builder.HasOne(e => e.ActionType)
            .WithMany()
            .HasForeignKey(e => e.ActionTypeId);
        builder.Property(e => e.Fee).HasColumnName("fee");
        builder.HasData(CreateActionFee());
    }
    
    private ActionFee[] CreateActionFee()
    {
        return new ActionFee[]
        {
            new ActionFee(id: 1, accountTypeId: 1, actionTypeId: 1, fee: 0),
            new ActionFee(id: 2, accountTypeId: 2, actionTypeId: 1, fee: 0),
            new ActionFee(id: 3, accountTypeId: 3, actionTypeId: 1, fee: 0),
            new ActionFee(id: 4, accountTypeId: 1, actionTypeId: 2, fee: 30),
            new ActionFee(id: 5, accountTypeId:2, actionTypeId: 2, fee: 20),
            new ActionFee(id: 6, accountTypeId:3, actionTypeId: 2, fee: 1),
            new ActionFee(id: 7, accountTypeId: 1, actionTypeId: 3, fee: 25),
            new ActionFee(id: 8, accountTypeId:2, actionTypeId: 3, fee: 15),
            new ActionFee(id: 9, accountTypeId:3, actionTypeId: 3, fee: 5),
            new ActionFee(id: 10, accountTypeId: 1, actionTypeId: 4, fee: 0),
            new ActionFee(id: 11, accountTypeId:2, actionTypeId: 4, fee: 0),
            new ActionFee(id: 12, accountTypeId:3, actionTypeId: 4, fee: 0),
        };
    }
}