namespace WalletV2.Models;

public class AccountType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public AccountType(int id)
    {
        Id = id;
        Name = string.Empty;
    }

    public AccountType(int id, string name)
    {
        Id = id;
        Name = name;
    }
}