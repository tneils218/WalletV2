namespace WalletV2.Models;

public class WalletHistory
{
    public int Id { get; set; }
    
    public Wallet? Wallet { get; set; }
    
    public int WalletId { get; set; }

    public decimal Amount { get; set; }

    public AccountType? AccountType { get; set; } = null!;

    public ActionType? ActionType { get; set; } = null!;

    public int AccountTypeId { get; set; }

    public int ActionTypeId { get; set; }

    public int? SourceWalletId { get; set; }
    
    public int? DestinationWalletId { get; set; }

    public decimal Fee { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}