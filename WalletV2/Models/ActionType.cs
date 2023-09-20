namespace WalletV2.Models;

public class ActionType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public ActionType(int id, string name)
    {
        Id = id;
        Name = name;
    }
}