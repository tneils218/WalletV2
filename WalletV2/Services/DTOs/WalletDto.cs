namespace WalletV2.Services.DTOs;

public class WalletDto
{
    public int AccountId { get; set; }
    public decimal Amount { get; set; }

    private WalletDto(int accountId)
    {
        AccountId = accountId;
    }

    public static WalletDto Create(int accountId)
    {
        return new WalletDto(accountId);
    }
}